using UnityEngine;

public class ReboundProjectile : MonoBehaviour, IActivatable, IReset
{
    public bool IsActivatedOnStart {get; set;}
    public bool IsActivated {get; set;}

    [SerializeField] private float _speed = 1;
    [SerializeField] private bool _isActivatedOnStart;  
    [SerializeField] private Transform _spawnPoint;
    private Rigidbody2D _rb;
    
    public void Activate()
    {
        BeginMovement();
    }
    public void Deactivate()
    {
        StopMovement();
    }
    public void Reset()
    {
        transform.position = _spawnPoint.position;
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
        if (_isActivatedOnStart)
        {
            Activate();
        }
    }
    private void OnDisable()
    {
        UnregisterFromManager();
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (_isActivatedOnStart)
        {
            Activate();
        }
    }

    private void BeginMovement()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        _rb.linearVelocity = randomDirection * _speed;
    }
    private void StopMovement()
    {
        _rb.linearVelocity = Vector2.zero;
    }
}
