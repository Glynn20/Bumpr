using UnityEngine;
[CreateAssetMenu(fileName ="New BreakableWallConfig", menuName = "Config/BreakableWall")]
public class BreakableWallConfig : ScriptableObject
{
    
    [Range(1,100)]public int MaxHealth;
}
