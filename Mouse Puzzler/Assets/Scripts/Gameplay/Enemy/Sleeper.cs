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

    private bool _playerInRange = false;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rb;
    private Transform _transform;
    private Vector2 _chaseDirection;

    // Default Values
    private bool _defaultIsActivatedOnStart;

    public void Reset()
    {
        _transform.position = _spawnLocation.position;
        IsAggro = false;
        _rb.linearVelocity = Vector2.zero;

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

        if (IsAggro)
        {
            ChasePlayer();
            return;
        }
        else
        {
            if (_playerInRange)
            {
                CheckForPlayerRunning();
            }
        }
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
            _playerInRange = true;
            if (_instantlyChaseWhenPlayerInRange) IsAggro = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            _playerInRange = false;
        }
    }
    private void ChasePlayer()
    {
        _chaseDirection = (_playerMovement.transform.position - _transform.position).normalized;
        _rb.linearVelocity = _chaseDirection * _moveSpeed; 
        //_transform.position = Vector2.MoveTowards(_transform.position, _playerMovement.transform.position, _moveSpeed * Time.deltaTime);
    }
    
    private void CheckForPlayerRunning()
    {
        PlayerMoveState state = _playerMovement.GetMoveState();
        if (state == PlayerMoveState.Running ||
            state == PlayerMoveState.Overheating)
        {
            IsAggro = true;
        }
    }
}
