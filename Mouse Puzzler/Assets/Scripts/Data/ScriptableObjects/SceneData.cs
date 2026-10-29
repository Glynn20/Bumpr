using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneData", menuName = "Game/Scene Data")]
public class SceneData : ScriptableObject
{
    public string SceneName; // Must map onto the corresponding scene's actual name
    public string InGameDisplayName;

    [Header("Config")]
    
    public SoundData BGMTrack;
    public SceneColourData ColourData;
    public LevelType levelType;

    [Header("Rankings")]
    
    // Time to beat Rankings
    public int STime;
    public int ATime;
    public int BTime;
    public int CTime;

    public enum LevelType
    {
        Normal = 1,
        Challenge = 2,
        Nightmare = 3
    }
}
