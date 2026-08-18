using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneColourData", menuName = "Game/Scene Colour Data")]
public class SceneColourData : ScriptableObject
{
    [Header("Config")]
    
    public Color lockedNormalColour = new Color(73 / 255f, 73 / 255f, 73 / 255f);
    public Color lockedChallengeColour = new Color(73 / 255f, 0 / 255f, 0 / 255f);
    public Color lockedNightmareColour = new Color(73 / 255f, 0 / 255f, 73 / 255f);
    
    [Space]

    public Color unlockedNormalColour = new Color(140 / 255f, 140 / 255f, 140 / 255f);
    public Color unlockedChallengeColour = new Color(140 / 255f, 0 / 255f, 0 / 255f);
    public Color unlockedNightmareColour = new Color(140 / 255f, 0 / 255f, 140 / 255f);

}
