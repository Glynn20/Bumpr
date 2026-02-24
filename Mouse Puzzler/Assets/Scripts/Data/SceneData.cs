using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneData", menuName = "Game/Scene Data")]
public class SceneData : ScriptableObject
{
    public string SceneName;
    public int LevelNumber;
}
