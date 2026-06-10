using JetBrains.Annotations;
using UnityEngine;

public class DeathInfo 
{
    public Transform DeathLocation {get; private set;} 

    public DeathInfo(Transform deathLocation)
    {
        DeathLocation = deathLocation;
    }
}
