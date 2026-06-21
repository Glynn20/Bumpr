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

    [Header("Data")]
    [SerializeField] private PlayerData _data;


    [Header("Bomb Implementation")]
    [SerializeField] private Transform _bombAttachPoint;
    [SerializeField] private bool _isPermanentAttachPoint = false;

    [Header("Game Events")]
    [SerializeField] private GameEvent _playerDeathEvent;

    private Transform _transform;
    private int _currentHealth;
    private int _maxHealth = 1;

    void Awake()
    {
        _transform = transform;
        _maxHealth = _data.MaxHealth;
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
    public void TakeDamage() 
    {
        _currentHealth -= Constants.UNIVERSALDAMAGEVALUE;
        
        if (_currentHealth <= 0) Die();
    } 
    public void Die()
    {  
        _playerDeathEvent?.Raise(this, new DeathInfo(_transform));
    }

}
