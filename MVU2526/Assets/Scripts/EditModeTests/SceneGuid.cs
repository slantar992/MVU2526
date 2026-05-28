using System;
using UnityEditor;

[Serializable]
public class SceneGuid
{
    public string guid;



    public override string ToString()
    {
        return AssetDatabase.GUIDToAssetPath(guid);
    }

    public static implicit operator string(SceneGuid obj) => obj.guid;
}
