using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "SceneRegistry", menuName = "Game/Scene Registry")]
public class SceneRegistry : ScriptableObject
{
    public List<SceneData> AllScenes;
    public List<SceneData> AllGameplayLevels;

    public SceneData GetLevelByName(string levelName)
    {
        return AllScenes.Find(s => s.SceneName == levelName);
    }

}
