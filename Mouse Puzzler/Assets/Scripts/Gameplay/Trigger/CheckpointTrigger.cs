using UnityEngine;

public class CheckpointTrigger : MonoBehaviour, IReset
{
    [Header("Events")]
    [SerializeField] private GameEvent _onCheckpointReached;
    
    [Header("Transforms")]
    [SerializeField] private Transform _camSnapPoint;
    [SerializeField] private Transform _spawnPoint;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _preTriggerSprite;
    [SerializeField] private Sprite _onTriggerSprite;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;


    public void Reset()
    {
        _collider.enabled = true;
        _spriteRenderer.sprite = _preTriggerSprite;
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
    
    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = _preTriggerSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;

        if (_camSnapPoint == null || _spawnPoint == null)
        {
            Debug.LogWarning("Invalid Checkpoint. Set transforms in inspector.", this);
            return;
        }

        _onCheckpointReached?.Raise(this, new Checkpoint(_camSnapPoint, _spawnPoint));
        _spriteRenderer.sprite = _onTriggerSprite;
        
        _collider.enabled = false;
    }
}
    