using UnityEngine;

public class Player : MonoBehaviour, IPlayer, IKillable
{
    Transform _transform;

    void Awake()
    {
        _transform = transform;
    }
    public void TeleportTo(Transform destination)
    {
        _transform.position = destination.position;
    }
    public void Respawn(Transform respawnPoint)
    {
        //Placeholder for now
        TeleportTo(respawnPoint);
        //
    }

    public void Die()
    {
        Debug.Log("Player Died.", this); return;
    }
}
