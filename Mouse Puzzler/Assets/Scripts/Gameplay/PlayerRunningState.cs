using System;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerRunningState : BaseState<PlayerStateMachine.PlayerState, PlayerContext>  
{
    PlayerContext _context;
    public PlayerRunningState(PlayerStateMachine.PlayerState key, PlayerContext context) 
        : base(key, context)
    {}

    public override void Enter(){}
    public override void Exit(){}
    public override void UpdateState(){}
    public override PlayerStateMachine.PlayerState GetNextState()
    {
        if (_context.Movement.GetMoveState() == PlayerMoveState.Overheating)
        {
            return PlayerStateMachine.PlayerState.Overheating;
        }
        else if (_context.Movement.GetMoveState() == PlayerMoveState.Running)
        {
            return PlayerStateMachine.PlayerState.Running;
        }
        else if (_context.Movement.GetMoveState() == PlayerMoveState.Walking)
        {
            return PlayerStateMachine.PlayerState.Walking;
        }
        else if (_context.Movement.GetMoveState() == PlayerMoveState.Idle)
        {
            return PlayerStateMachine.PlayerState.Idle;
        }
        else
        {
            return StateKey;
        }
    }
}
