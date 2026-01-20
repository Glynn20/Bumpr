using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class BreakableWall : MonoBehaviour, IHealth 
{
    
    private int _currentHealth;
    private int _maxHealth;
    private float HealthPercentage => (float)_currentHealth / _maxHealth;
    private SpriteRenderer _renderer;

    [Header("Health Settings.")]
    [SerializeField] private BreakableWallConfig _config;
    [SerializeField] private Color _highHealthColour = new Color(243f, 233f, 166f);
    [SerializeField] private Color _mediumHealthColour = new Color(242f, 250f, 122f);
    [SerializeField] private Color _lowHealthColour = new Color(195f, 171f, 0f, 1.0f);
    [Range(0,1)][SerializeField] private float _highHealthThreshold = 0.67f;
    [Range(0,1)][SerializeField] private float _mediumHealthThreshold = 0.34f;


    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    void Awake()
    {
        _maxHealth = _config.MaxHealth;
        _currentHealth = _maxHealth;
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = _highHealthColour;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Must specifically be the Player
        IMovement mover = other.gameObject.GetComponent<IMovement>();
        if ( mover == null ) return;

        if ( mover.GetMoveState() == PlayerMoveState.Overheating )
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        _currentHealth -= Constants.UNIVERSALDAMAGEVALUE;
        
        if (_currentHealth <= 0)
        {
            Die();
            return;
        }

        if (HealthPercentage < _mediumHealthThreshold) 
        {
            _renderer.color = _lowHealthColour;
            return;
        } 
        if (HealthPercentage < _highHealthThreshold)   
        {
            _renderer.color = _mediumHealthColour;
            return;
        } 
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
