using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    SpriteRenderer _sprite;
    Rigidbody2D _rb;
    BoxCollider2D _collider;

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        HideAndDestroy();
    }

    void HideAndDestroy()
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
}
