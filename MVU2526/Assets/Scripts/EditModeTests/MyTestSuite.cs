using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class MyTestSuite
{
    public static List<SceneGuid> StableScenesGuids 
        => StableScenes.sceneGuid;
        
        
    /*    = new List<SceneGuid>()
    {
        new() { guid = "55f31f4963ce2064e8d60933a6f9d190" },
        new() { guid = "99c9720ab356a0642a771bea13969a05" },
        new() { guid = "b38435b0ee198ab479073dc871b6ac04" },
    };*/

    public static StableScenes StableScenes;

    static MyTestSuite()
    {
        StableScenes = AssetDatabase.LoadAssetByGUID<StableScenes>(new GUID("8823db61b5f76ad4f84dd3ab1e61e5f6"));
    }


    [Test]
    public void HealthWith15Points_Give5Damage_ResultIs10()
    {
        //Arrange
        var health = new Health(15);

        //Act
        health.Damage(5);

        //Assert
        Assert.That(health.Value, Is.EqualTo(10));
    }

    [Test, Ignore("no needed components, this is a test to show how to load an asset")]
    public void MyPrefabMyMenuInjectorComponent_ByDefinition_ContinueMessageIsFilled()
    {
        var myPrefab = AssetDatabase.LoadAssetByGUID<GameObject>(new GUID("088fd4670501b604390a445dc8fe7b8c"));
        var injector = myPrefab.GetComponent<MainMenuInjector>();

        Assert.That(injector.ContinueMessage, Is.Not.Null);
    }

    [Test]
    public void HealthWith15_Restore10_ResultIs25()
    {
        var health = new Health(15);

        health.Restore(10);

        Assert.That(health.Value, Is.EqualTo(25), "The health must be 25");
    }

    [TestCaseSource("StableScenesGuids")]
    public void UIScene_ByDefinition_ThereIsOnlyOneCamera(SceneGuid sceneGuid)
    {
        var scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
        EditorSceneManager.OpenScene(scenePath);
        var cameraList = GameObject.FindObjectsByType<Camera>(FindObjectsSortMode.None)
            .Select(c => $"  - {c.name}");

        if(cameraList.Count() != 1)
        {
            var message = "";

            if(cameraList.Count() == 0)
            {
                message = $"There is not cameras in scene {scenePath}";
            }
            else
            {
                message = $"There is more than 1 cameras in scene {scenePath}\nCamera list:\n";
                message += string.Join('\n', cameraList);
            }
            Assert.Fail(message);
        }

    }

}


public class Health
{
    public int Value { get; private set; }
    public Health(int v)
    {
        Value = v;
    }

    public void Damage(int amount)
    {
        Value -= amount;
    }

    public void Restore(int v)
    {
        Value += v;
    }
}