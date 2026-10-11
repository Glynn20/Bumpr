using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameEvent OnPlaySound;
    [SerializeField] private GameEvent OnPlayParticle;
    [SerializeField] private SoundData _hitSoundData;
    [SerializeField] private ParticleData _hitFX; 

    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private float _lifecycle = 5.0f;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        _lifecycle -= Time.deltaTime;
        if (_lifecycle <= 0.0f) Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        HideAndDestroy();
        OnHit(other.contacts[0].point);
    }

    private void HideAndDestroy()
    {
        if (_sprite == null)
        {
            Debug.Log("Projectile Sprite is null. Could not disable sprite component.", this);
            return;
        }
        _sprite.enabled = false;
        _rb.linearVelocity = Vector2.zero;
        _collider.enabled = false;
        Destroy(gameObject, 0.5f);
    }

    private void OnHit(Vector2 hitPosition)
    {
        if (_hitSoundData != null)
        {
            OnPlaySound?.Raise(this, _hitSoundData);
        }
        if (_hitFX != null)
        {
            OnPlayParticle?.Raise(this, _hitFX, hitPosition, transform.rotation.eulerAngles.z - 90f);
        }
    }

}
