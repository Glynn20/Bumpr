using UnityEngine;

public interface IMovement
{
    public float GetCurrentSpeed();
    public PlayerMoveState GetMoveState();
}
