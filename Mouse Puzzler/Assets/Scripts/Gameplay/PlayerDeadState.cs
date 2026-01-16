using System;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerDeadState : BaseState<PlayerStateMachine.PlayerState, PlayerContext>  
{

    public PlayerDeadState(PlayerStateMachine.PlayerState key, PlayerContext context) 
        : base(key, context)
    {}

    public override void Enter(){}
    public override void Exit(){}
    public override void UpdateState(){}
    public override PlayerStateMachine.PlayerState GetNextState()
    {
        return PlayerStateMachine.PlayerState.Dead;
    }
}
