using JetBrains.Annotations;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Sleeper : MonoBehaviour, IActivatable, IReset
{
    public bool IsActivatedOnStart 
    {
        get { return _isActivatedOnStart; } 
        set { _isActivatedOnStart = value; }
    }
    public bool IsActivated{get; set;}
    public bool IsAggro {get; private set;}

    [Header("Behaviour")]
    [SerializeField] private bool _isActivatedOnStart;
    [SerializeField] private bool _instantlyChaseWhenPlayerInRange;
    [SerializeField] private CircleCollider2D _detectionCollider;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _spawnLocation; 

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;

    [Header("Sounds")]
    [SerializeField] private SoundData _aggroSound;

    [Header("Animator")]
    [SerializeField] private string _idleAnimName;
    [SerializeField] private string _aggroAnimName;

    private bool _playerInRange = false;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rb;
    private Transform _transform;
    private Vector2 _chaseDirection;
    private Animator _animator;

    // Default Values
    private bool _defaultIsActivatedOnStart;

    public void PlayAggroSound()
    {
        OnPlaySound?.Raise(this, _aggroSound);
    }

    public void Reset()
    {
        Debug.Log("Resetted");
        _transform.position = _spawnLocation.position;
        IsAggro = false;
        _rb.linearVelocity = Vector2.zero;
        _animator?.Play(_idleAnimName);

        if (_defaultIsActivatedOnStart) Activate();
        else                            Deactivate();
    }

    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }
    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }
    private void OnEnable()
    {
        RegisterToManager();
    }
    private void OnDisable()
    {
        UnregisterFromManager();
    }
    public void Activate()
    {
        IsActivated = true;
    }
    public void Deactivate()
    {
        IsActivated = false;
        IsAggro = false;
        _rb.linearVelocity = Vector2.zero;
    }
    public void OnPlayerDeath()
    {
        Deactivate();
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _transform = transform;
        _defaultIsActivatedOnStart = IsActivatedOnStart;

        SetSpawn();

        _detectionCollider.radius = _detectionRadius;
        if (_isActivatedOnStart) Activate();
    }
    void Start()
    {
        _playerMovement = GameObject.FindFirstObjectByType<PlayerMovement>();
    }
    void Update()
    {
        if (!IsActivated) return;

        if (IsAggro) ChasePlayer();
    }

    private void SetSpawn()
    {
        if (_spawnLocation == null) _transform.position = _spawnLocation.position;
        else                        _transform.position = _spawnLocation.position;
    }

    void OnTriggerEnter2D(Collider2D other) // Player enters detection trigger radius
    {
        Debug.Log("OnTriggerEnter2D detected.", this);
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            if (IsAggro) return;

            else
            {
                IsAggro = true;
                _animator?.Play(_aggroAnimName);
            }
        }
    }

    private void ChasePlayer()
    {
        _chaseDirection = (_playerMovement.transform.position - _transform.position).normalized;
        _rb.linearVelocity = _chaseDirection * _moveSpeed; 

    }
    
}
