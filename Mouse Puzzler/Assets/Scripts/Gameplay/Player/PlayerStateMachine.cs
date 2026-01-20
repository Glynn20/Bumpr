using System;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.PlayerState, PlayerContext>  
{

    public enum PlayerState
    {
        Idle = 0,
        Walking = 1,
        Running = 2,
        Overheating = 3,
        Dizzy = 4,
        Dead = 5
    }

    protected override BaseState<PlayerStateMachine.PlayerState, PlayerContext> SetInitialState()
    {
        return _states[initialKey];
    }
    
    protected override void SetUpStates()
    {
        _states.Add(PlayerState.Idle,        new PlayerIdleState(PlayerState.Idle, _context));
        _states.Add(PlayerState.Walking,     new PlayerWalkingState(PlayerState.Walking, _context));
        _states.Add(PlayerState.Running,     new PlayerRunningState(PlayerState.Running, _context));
        _states.Add(PlayerState.Overheating, new PlayerOverheatingState(PlayerState.Overheating, _context));
        _states.Add(PlayerState.Dizzy,       new PlayerDizzyState(PlayerState.Dizzy, _context));
        _states.Add(PlayerState.Dead,        new PlayerDeadState(PlayerState.Dead, _context));
    }

}

