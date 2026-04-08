using UnityEngine;
[CreateAssetMenu(fileName ="New ProjectileConfig", menuName = "Config/Projectile")]
public class ProjectileConfig : ScriptableObject
{
    public GameObject Prefab;
    public float Velocity;
}
