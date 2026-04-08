using JetBrains.Annotations;
using TMPro;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class Sleeper : MonoBehaviour, IActivatable
{
    public bool IsActivatedOnStart 
    {
        get { return _isActivatedOnStart; } 
        set { _isActivatedOnStart = value; }
    }
    public bool IsActivated{get; set;}
    public bool IsAggro {get; private set;}
    [SerializeField] private bool _isActivatedOnStart;
    [SerializeField] private CircleCollider2D _detectionCollider;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _moveSpeed = 5f;
    private bool _playerInRange = false;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rb;
    private Transform _transform;
    private Vector2 _chaseDirection;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = transform;
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

        if (_playerInRange)
        {
            CheckForPlayerRunning();
        }
    }

    void OnTriggerEnter2D(Collider2D other) // Player enters detection trigger radius
    {
        Debug.Log("OnTriggerEnter2D detected.", this);
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            _playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            _playerInRange = false;
        }
    }

    public void Activate()
    {
        IsActivated = true;
    }
    public void Deactivate()
    {
        IsActivated = false;
        _rb.linearVelocity = Vector2.zero;
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
