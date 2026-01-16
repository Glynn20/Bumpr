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

}
