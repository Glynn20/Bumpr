using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour, IEnemy
{

    /*
    SUMMARY

    Stationary
    Shoot bullet coroutine(int shotperburst)

    Timer

    timer hits zero

    start coroutine

    */
    [SerializeField] private bool _isEnabledOnStart = false;

    [Header("Shooting Config")]
    [SerializeField] private float _timeBetweenAttack = 1.0f;
    [SerializeField] private float _timeBetweenIndividualShot = 0.4f;
    [SerializeField] private int _shotsPerBurst = 1;
    [SerializeField] private Transform socket;

    [Header("Projectile")]
    [SerializeField] private ProjectileConfig _config;

    private float _remainingTimeBetweenAttack;
    private bool _isEnabled;
    private Coroutine _shootCoroutine = null;
    private WaitForSeconds _burstWait;

    void Awake()
    {
        _isEnabled = _isEnabledOnStart;
        _remainingTimeBetweenAttack = _timeBetweenAttack;
        _burstWait = new WaitForSeconds(_timeBetweenIndividualShot);
    }
    void Update()
    {
        if (!_isEnabled || _shootCoroutine != null) return;

        _remainingTimeBetweenAttack -= Time.deltaTime;

        if (_remainingTimeBetweenAttack > 0) return;
        
        if (_shootCoroutine != null) return;
        _shootCoroutine = StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        for (int i = 0; i < _shotsPerBurst; ++i)
        {
            SpawnProjectile();
            yield return _burstWait;
        }

        _shootCoroutine = null;
        _remainingTimeBetweenAttack = _timeBetweenAttack;
        yield return null;
    }

    private void SpawnProjectile()
    {
        GameObject instance = Object.Instantiate(_config.Prefab, socket.position, socket.rotation);
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        
        if (rb == null) 
        {
            Debug.Log("Projectile RigidBody is null.", this);
            return;
        }
        
        rb.linearVelocity = socket.up * _config.Velocity;
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }
}
