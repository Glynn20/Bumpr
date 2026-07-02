using UnityEngine;

public class CheckpointTrigger : MonoBehaviour//, IReset
{
    [Header("Events")]
    [SerializeField] private GameEvent _onCheckpointReached;
    [SerializeField] private GameEvent OnPlaySound;
    
    [Header("Transforms")]
    [SerializeField] private Transform _camSnapPoint;
    [SerializeField] private Transform _spawnPoint;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _preTriggerSprite;
    [SerializeField] private Sprite _onTriggerSprite;

    [Header("Sound")]
    [SerializeField] private SoundData _onTriggerSoundData;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private bool _triggered = false;

/*
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
    */
    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = _preTriggerSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;
        if (_triggered) return;
        if (_camSnapPoint == null || _spawnPoint == null)
        {
            Debug.LogWarning("Invalid Checkpoint. Set transforms in inspector.", this);
            return;
        }

        _triggered = true;
        _onCheckpointReached?.Raise(this, new Checkpoint(_camSnapPoint, _spawnPoint));
        OnPlaySound?.Raise(this, _onTriggerSoundData);

        _spriteRenderer.sprite = _onTriggerSprite;
        
        _collider.enabled = false;
    }
}
    