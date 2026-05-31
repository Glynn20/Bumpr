using UnityEngine;

public class Shooter : MonoBehaviour, IActivatable, IReset
{

    public bool IsActivated{get; set;}
    public bool IsActivatedOnStart
    {
        get {return _isActivatedOnStart;} 
        set { _isActivatedOnStart = value;}
    }

    [SerializeField] private bool _isActivatedOnStart;

    [Header("Behaviour")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSocket;
    [Range(0.5f,10.0f)][SerializeField] private float _shotsPerSecond;
    [SerializeField] private float _shotSpeed;
    
    [Header("Sound")]
    [SerializeField] private SoundData _shooterShootSoundData;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;


    [Header("Other")]
    [SerializeField] private Player _playerRef;

    private float _timeBeforeNextShot;

    // Default Values
    private bool _defaultIsActivatedOnStart;
    
    public void Reset()
    {
        if (_defaultIsActivatedOnStart) Activate();
        else                            Deactivate();
        ResetShotTime();
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

    }

    public void Update()
    {
        _timeBeforeNextShot -= Time.deltaTime;

        if (IsActivated && _timeBeforeNextShot <= 0f)
        {
            ShootPlayer();
            ResetShotTime();
        }
    }

    private void Awake()
    {
        if (_defaultIsActivatedOnStart) Activate();
        else                            Deactivate();
        ResetShotTime();
    }

    private void ShootPlayer()
    {
        GameObject instance = Object.Instantiate(_projectilePrefab, _projectileSocket.position, _projectileSocket.rotation);
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

        OnPlaySound?.Raise(this, _shooterShootSoundData);

        if (rb == null || _playerRef == null)
        {
            Debug.LogWarning("Shooter is missing rigidbody for projectile or player reference. Double check.", this);
            return;
        }

        Vector2 direction = _playerRef.transform.position - this.transform.position;
        rb.linearVelocity  = direction.normalized * _shotSpeed;

        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        instance.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
    private void ResetShotTime()
    {
        _timeBeforeNextShot = 1.0f / _shotsPerSecond;
    }
}   
 