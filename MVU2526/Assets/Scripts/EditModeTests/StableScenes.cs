

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

[CreateAssetMenu(fileName = "StableScenes", menuName = "Tests/Stable scenes config")]
public class StableScenes : ScriptableObject
{
    public List<SceneGuid> sceneGuid = new List<SceneGuid>();
}

[CustomEditor(typeof(StableScenes))]
public class StableScenesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Reload scripts"))
        {
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}