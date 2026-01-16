using UnityEngine;
[CreateAssetMenu(fileName ="New PlayerMovementConfig", menuName = "Config/PlayerMovement")]
public class PlayerMovementConfig : ScriptableObject
{
    public float idleToWalkTransitionThreshold;
    public float walkToRunTransitionThreshold;
    public float runToOverheatThreshold;
}
