using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class ProximityInvisibility : MonoBehaviour, IReset
{
    private SpriteRenderer _renderer;
    [SerializeField] private BoxCollider2D _detectionCollider;

    [SerializeField] private float _detectionRange = 0.5f;

    private bool _revealed;

    public void Reset()
    {
        _renderer.enabled = false;
        _revealed = false;
    }
    public void RegisterToManager()
    {
        LevelManager.RegisterResettable(this);
    }
    public void UnregisterFromManager()
    {
        LevelManager.UnregisterResettable(this);
    }
    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _renderer.enabled = false;
        _revealed = false;
        _detectionCollider.isTrigger = true;
        _detectionCollider.edgeRadius = _detectionRange;
    }
    private void OnEnable()
    {
        RegisterToManager();
    }
    private void OnDisable()
    {
        UnregisterFromManager();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IPlayer>() == null)
        {
            return;
        }
        else if (_revealed == false)
        {
            _renderer.enabled = true;
            _revealed = true;
        }
    }
}
