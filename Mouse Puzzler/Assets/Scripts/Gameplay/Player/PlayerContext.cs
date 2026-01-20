using UnityEngine;

public class PlayerContext
{
    public PlayerMovement Movement;
    public Player Player;
    public PlayerContext(Player player, PlayerMovement movement)
    {   
        Player = player;
        Movement = movement;
    }
}
