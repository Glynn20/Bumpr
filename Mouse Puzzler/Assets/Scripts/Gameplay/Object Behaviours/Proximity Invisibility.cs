using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class ProximityInvisibility : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _detectionCollider;

    [SerializeField] private float _detectionRange = 0.5f;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.enabled = false;
        _detectionCollider.isTrigger = true;
        _detectionCollider.edgeRadius = _detectionRange;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IPlayer>() == null)
        {
            return;
        }
        else
        {
            _spriteRenderer.enabled = true;
            this.enabled = false;
        }
    }
}
