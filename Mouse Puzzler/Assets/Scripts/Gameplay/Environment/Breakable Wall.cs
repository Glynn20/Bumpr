using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(SpriteRenderer))]
public class BreakableWall : MonoBehaviour,
IHealth,
IReset 
{
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    [SerializeField] private Collider2D _physicalCollider; // Must manually set in Inspector

    [Header("Health Settings.")]
    [SerializeField] private BreakableWallConfig _config;
    [SerializeField] private Color _highHealthColour = new Color(243f, 233f, 166f);
    [SerializeField] private Color _mediumHealthColour = new Color(242f, 250f, 122f);
    [SerializeField] private Color _lowHealthColour = new Color(195f, 171f, 0f, 1.0f);
    [Range(0,1)][SerializeField] private float _highHealthThreshold = 0.67f;
    [Range(0,1)][SerializeField] private float _mediumHealthThreshold = 0.34f;

    [Header("Sound")]
    [SerializeField] private SoundData _onHitSoundData;
    [SerializeField] private SoundData _onBreakSoundData;

    [Header("ParticleFX")]
    [SerializeField] private ParticleData _wallHitFX;
    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;
    [SerializeField] private GameEvent OnPlayHitParticle;

    private int _currentHealth;
    private int _maxHealth;
    private float HealthPercentage => (float)_currentHealth / _maxHealth;
    private SpriteRenderer _renderer;
    
    public void Reset()
    {
        if (GetComponent<ProximityInvisibility>() == null) _renderer.enabled = true;
        
        _physicalCollider.enabled = true;
        _currentHealth = _maxHealth;
        ChangeColour(_highHealthColour);
    }
    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }
    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }
    public void TakeDamage()
    {
        _currentHealth -= Constants.UNIVERSALDAMAGEVALUE;
        
        if (_currentHealth <= 0)
        {
            Die();
            OnPlaySound?.Raise(this, _onBreakSoundData);
            return;
        }

        OnPlaySound?.Raise(this, _onHitSoundData);

        if (HealthPercentage < _mediumHealthThreshold) 
        {
            ChangeColour(_lowHealthColour);
            return;
        } 
        else if (HealthPercentage < _highHealthThreshold)   
        {
            ChangeColour(_mediumHealthColour);
            return;
        } 
    }

    public void Die()
    {
        _physicalCollider.enabled = false;
        _renderer.enabled = false;
    }

    private void OnEnable()
    {
        RegisterToManager();
    }
    private void OnDisable()
    {
        UnregisterFromManager();
    }

    private void Awake()
    {
        _maxHealth = _config.MaxHealth;
        _currentHealth = _maxHealth;
        _renderer = GetComponent<SpriteRenderer>();
        ChangeColour(_highHealthColour);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
         IMovement mover = other.gameObject.GetComponent<IMovement>();
        if ( mover == null ) return;

        if ( mover.GetMoveState() == PlayerMoveState.Overheating )
        {
            TakeDamage();
            PlayHitFX(other.contacts[0].point);
        }
    }
    private void PlayHitFX(Vector2 position)
    {
        if (_wallHitFX == null) return; 
        OnPlayHitParticle?.Raise(this, _wallHitFX, position); 
    }
    private void ChangeColour(Color newColour)
    {
        _renderer.color = newColour;
    }


}
