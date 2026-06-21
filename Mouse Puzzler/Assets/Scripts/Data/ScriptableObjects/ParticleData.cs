using UnityEngine;
[CreateAssetMenu(fileName = "New Particle Data", menuName = "Data/Particle")]
public class ParticleData : ScriptableObject
{
    public int defaultPoolSize;
    public ParticleSystem prefab;
}
