using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class ProximityInvisibility : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;

    [SerializeField] private float _revealSpriteRange = 1.0f;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();

        _spriteRenderer.enabled = false;
        _collider.radius = _revealSpriteRange;
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
