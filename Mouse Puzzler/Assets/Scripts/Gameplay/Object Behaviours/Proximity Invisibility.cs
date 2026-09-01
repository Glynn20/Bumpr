using UnityEngine;


public class ProximityInvisibility : MonoBehaviour, IReset
{
    private SpriteRenderer[] _renderers;
    [SerializeField] private BoxCollider2D _detectionCollider;

    [SerializeField] private float _detectionRange = 0.5f;

    private bool _revealed;

    public void Reset()
    {
        SetRenderersEnabled(false);
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
        _renderers = GetComponentsInChildren<SpriteRenderer>();

        SetRenderersEnabled(false);
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
            SetRenderersEnabled(true);
            _revealed = true;
        }
    }

    private void SetRenderersEnabled(bool state)
    {
        foreach (SpriteRenderer spr in _renderers)
        {
            spr.enabled = state;
        }
    }
}
