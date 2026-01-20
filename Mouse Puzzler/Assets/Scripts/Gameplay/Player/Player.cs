using UnityEngine;

public class Player : MonoBehaviour, 
    IPlayer,
    IHealth,
    ICanAttachBomb
{
    private Transform _transform;
    private int _currentHealth;
    private int _maxHealth;
    [SerializeField] private Transform _bombAttachPoint;
    [SerializeField] private bool _isPermanentAttachPoint = false;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public Transform BombAttachPoint => _bombAttachPoint;
    public bool IsPermanentAttachPoint => _isPermanentAttachPoint;

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
    public void TakeDamage() { Die(); } 
    public void Die()
    {
        Debug.Log("Player Died.", this); return;
    }
}
