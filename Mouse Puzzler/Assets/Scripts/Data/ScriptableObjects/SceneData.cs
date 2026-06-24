using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneData", menuName = "Game/Scene Data")]
public class SceneData : ScriptableObject
{
    public string SceneName;
    public int LevelNumber; // To be used by SceneController
    public bool isMainMenu;
    public bool IsUnlocked = false;
    public SoundData BGMTrack;
}
