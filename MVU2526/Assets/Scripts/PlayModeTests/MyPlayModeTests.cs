using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class MyPlayModeTests
{
    private List<string> errorMessages = new List<string>();
    private List<string> warningMessages = new List<string>();

    public static List<SceneGuid> StableScenesGuids = new List<SceneGuid>()
    {
        new() { guid = "55f31f4963ce2064e8d60933a6f9d190" },
        new() { guid = "99c9720ab356a0642a771bea13969a05" },
        new() { guid = "b38435b0ee198ab479073dc871b6ac04" },
    };

    public class SceneGuid
    {
        public string guid;

        public override string ToString()
        {
            return GetScenePath();
        }

        public string GetScenePath()
        {
            return AssetDatabase.GUIDToAssetPath(guid);
        }

        public static implicit operator string(SceneGuid obj) => obj.guid;
    }

    [TearDown]
    public void TearDown()
    {
        Application.logMessageReceived -= Application_logMessageReceived;
    }

    [SetUp]
    public void Setup()
    {
        errorMessages.Clear();
        warningMessages.Clear();
        Application.logMessageReceived += Application_logMessageReceived;
    }

    [UnityTest]
    public IEnumerator UIScene_Stay1seconds_NoErrorsOrWarnings()
    {
        LogAssert.ignoreFailingMessages = true;
        var scene = new SceneGuid() { guid = "55f31f4963ce2064e8d60933a6f9d190" };
        EditorSceneManager.LoadScene(scene.GetScenePath());

        yield return new WaitForSeconds(1);

        if (errorMessages.Count > 0 || warningMessages.Count > 0) 
        {
            var message = "There are some errors or warnings on scene\n";
            if (errorMessages.Count > 0) 
            {
                message += "Errors:\n";
                message += string.Join("\n\n", errorMessages);
            }

            if(warningMessages.Count > 0)
            {
                message += "Warnings:\n";
                message += string.Join("\n\n", warningMessages);
            }
            Assert.Fail(message);
        }

    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Assert:
            case LogType.Exception:
            case LogType.Error:
                errorMessages.Add(condition + "\n" + stackTrace);
                break;
            case LogType.Warning:
                warningMessages.Add(condition + "\n" + stackTrace);
                break;
        }
    }
}
