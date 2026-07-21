using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, 
    IPlayer,
    IHealth,
    ICanAttachBomb
{
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public Transform BombAttachPoint => _bombAttachPoint;
    public bool IsPermanentAttachPoint => _isPermanentAttachPoint;
    public bool IsDead => _isDead;

    [Header("Data")]
    [SerializeField] private PlayerData _data;


    [Header("Bomb Implementation")]
    [SerializeField] private Transform _bombAttachPoint;
    [SerializeField] private bool _isPermanentAttachPoint = false;

    [Header("Game Events")]
    [SerializeField] private GameEvent _playerDeathEvent;

    private Transform _transform;
    private Collider2D _collider;
    private int _currentHealth;
    private int _maxHealth = 1;
    private bool _isDead;

    void Awake()
    {
        _transform = transform;
        _maxHealth = _data.MaxHealth;
        _collider = GetComponent<BoxCollider2D>();
    }
    
    public void TeleportTo(Transform destination)
    {
        _transform.position = destination.position;
    }
    public void Respawn()
    {
        _isDead = false;
        EnableCollider();
    }
    public void TakeDamage() 
    {
        _currentHealth -= Constants.UNIVERSALDAMAGEVALUE;
        
        if (_currentHealth <= 0) Die();
    } 
    public void DisableCollider()
    {
        _collider.enabled = false;
    }
    public void EnableCollider()
    {
        _collider.enabled = true;
    }
    public void Die()
    { 
        _isDead = true;
        DisableCollider();
        _playerDeathEvent?.Raise(this, new DeathInfo(_transform));
    }

}
