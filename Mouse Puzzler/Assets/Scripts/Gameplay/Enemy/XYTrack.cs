using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class XYTrack : MonoBehaviour, IActivatable, IReset
{
    public enum TrackingAxis
    {
        X = 0,
        Y = 1
    }

    public bool IsActivatedOnStart 
    {
        get { return _isActivatedOnStart; } 
        set { _isActivatedOnStart = value; }
    }
    public bool IsActivated{get; set;}

    [Header("Behaviour")]
    
    [SerializeField] private bool _isActivatedOnStart;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private TrackingAxis _trackingAxis; 
    private bool _defaultIsActivatedOnStart;
    private PlayerMovement _playerMovement;
    private Vector2 _chaseDirection;
    private Transform _transform;
    private Rigidbody2D _rb;

    public void OnPlayerDeath()
    {
        Deactivate();
    }
    public void Reset()
    {
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
        _rb.linearVelocity = Vector2.zero;
    }
    
    void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;

        if (_trackingAxis == TrackingAxis.X)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    void Start()
    {
        _playerMovement = GameObject.FindFirstObjectByType<PlayerMovement>();
    }

    
    void Update()
    {
        if (!IsActivated) return;

        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (_playerMovement == null) return;
        _chaseDirection = (_playerMovement.transform.position - _transform.position).normalized;
        _rb.linearVelocity = _chaseDirection * _moveSpeed; 
    }
}
