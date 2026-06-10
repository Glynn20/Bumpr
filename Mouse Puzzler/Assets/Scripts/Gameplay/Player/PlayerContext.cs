using UnityEngine;

public class PlayerContext
{
    public PlayerMovement Movement;
    public Player Player;
    public PlayerAnimations Animations;
    public PlayerContext(Player player, PlayerMovement movement, PlayerAnimations animations)
    {   
        Player = player;
        Movement = movement;
        Animations = animations;
    }
}
