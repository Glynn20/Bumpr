using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
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
        Debug.Log(other.gameObject.layer);
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


}
