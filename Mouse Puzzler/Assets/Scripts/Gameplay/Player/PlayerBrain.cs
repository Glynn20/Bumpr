using System;

using UnityEngine;

public class PlayerBrain: Brain<PlayerStateMachine, PlayerStateMachine.PlayerState, PlayerContext> 

{
    protected override PlayerContext InitializeContext()
    {
        return new PlayerContext(
            GetComponentInChildren<Player>(),
            GetComponentInChildren<PlayerMovement>(),
            GetComponentInChildren<PlayerAnimations>()
            );
    }
    
    public void OnPlayerDeath()
    {
        _stateMachine.TransitionToState(PlayerStateMachine.PlayerState.Dead);
        _context.Movement.DisableInput();
        _context.Player.DisableCollider();
        _context.Animations.TriggerDeath();
    }
    
    public void OnPlayerContinue()
    {
        _stateMachine.TransitionToState(_stateMachine.InitialKey);
        _context.Movement.EnableInput();
        _context.Player.EnableCollider();
        _context.Animations.ResetToEnterState();
    }

}
