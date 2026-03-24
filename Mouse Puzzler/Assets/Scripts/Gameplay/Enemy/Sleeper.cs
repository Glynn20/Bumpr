using JetBrains.Annotations;
using UnityEngine;

public class Sleeper : MonoBehaviour, IActivatable
{
    public bool IsActivatedOnStart 
    {
        get { return _isActivatedOnStart; } 
        set { _isActivatedOnStart = value; }
    }
    public bool IsActivated{get; set;}
    public bool IsAggro {get; private set;}

    
    private bool _playerInRange = false;
    private Transform _transform;
    private PlayerMovement _playerMovement;
    [SerializeField] private bool _isActivatedOnStart;
    [SerializeField] private CircleCollider2D _detectionCollider;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _moveSpeed = 5f;

    void Awake()
    {
        _transform = transform;
        _detectionCollider.radius = _detectionRadius;
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
    }

    private void ChasePlayer()
    {
        _transform.position = Vector2.MoveTowards(_transform.position, _playerMovement.transform.position, _moveSpeed * Time.deltaTime);
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
