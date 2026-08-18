using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "SceneRegistry", menuName = "Game/Scene Registry")]
public class SceneRegistry : ScriptableObject
{
    public List<SceneData> AllLevels;

    public SceneData GetLevelByName(string levelName)
    {
        return AllLevels.Find(s => s.SceneName == levelName);
    }

}
