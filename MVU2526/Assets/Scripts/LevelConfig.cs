

using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public string logicScenePath;
    public string audioScenePath;
    public string artScenePath;
    public string designScenePath;
}