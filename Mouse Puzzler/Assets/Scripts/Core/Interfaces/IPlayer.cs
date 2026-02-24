using UnityEngine;

public interface IPlayer 
{
    public void TeleportTo(Transform teleportDestination);
    public void Respawn(Transform respawnPoint);

}
