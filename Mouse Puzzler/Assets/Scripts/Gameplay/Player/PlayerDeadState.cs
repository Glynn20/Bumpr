using System;
using UnityEngine;

public class PlayerDeadState : BaseState<PlayerStateMachine.PlayerState, PlayerContext>  
{
    PlayerContext _context;
    public PlayerDeadState(PlayerStateMachine.PlayerState key, PlayerContext context) 
        : base(key, context)
    {
        _context = context;
    }

    public override void Enter(){}
    public override void Exit(){}
    public override void UpdateState(){}
    public override PlayerStateMachine.PlayerState GetNextState()
    {
        return PlayerStateMachine.PlayerState.Dead;
    }
}
