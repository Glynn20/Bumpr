using UnityEngine;
[CreateAssetMenu(fileName ="New PlayerMovementConfig", menuName = "Config/PlayerMovement")]
public class PlayerMovementConfig : ScriptableObject
{
    public float minDistanceToPlaySound;
    public float IdleToWalkTransitionThreshold;
    public float WalkToRunTransitionThreshold;
    public float RunToOverheatThreshold;
}
