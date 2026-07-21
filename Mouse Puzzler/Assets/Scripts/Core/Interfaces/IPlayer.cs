using UnityEngine;

public interface IPlayer 
{
    public void TeleportTo(Transform teleportDestination);
    public void Respawn();
    public void Die();

}
