using System;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerBrain: Brain<PlayerStateMachine, PlayerStateMachine.PlayerState, PlayerContext> 

{
    protected override PlayerContext InitializeContext()
    {
        return new PlayerContext(
            GetComponentInChildren<Player>(),
            GetComponentInChildren<PlayerMovement>()
            );
    }
    
    public void OnPlayerDeath()
    {
        _stateMachine.TransitionToState(PlayerStateMachine.PlayerState.Dead);
        _context.Movement.SetInputEnabled(false);
    }
    
    public void OnPlayerContinue()
    {
        _stateMachine.TransitionToState(_stateMachine.InitialKey);
        _context.Movement.SetInputEnabled(true);
    }

}
