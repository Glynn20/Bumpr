using UnityEngine;

public class Checkpoint
{
    public Transform CamSnapPoint { get; private set; }
    public Transform SpawnPoint { get; private set; }

    public Checkpoint(Transform camSnapPoint, Transform spawnPoint)
    {
        CamSnapPoint = camSnapPoint;
        SpawnPoint = spawnPoint;
    }

}