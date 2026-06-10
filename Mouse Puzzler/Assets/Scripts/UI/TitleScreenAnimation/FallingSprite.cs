using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.U2D;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class FallingSprite : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    public void Initialize(FallingSpriteData data)
    {
        float proportion = data.GetRandomScaleAndSpeedProportion();


        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = data.Sprites[Random.Range(0, data.Sprites.Length - 1)];
        _renderer.sortingOrder = data.OrderInLayer;
        float finalAlpha = data.MinAlpha + data.GetAlphaSpread() * proportion;
        _renderer.color = new Color(1,1,1, finalAlpha / 255);
        
        float finalScale = data.MinSize + (data.GetScaleSpread() * proportion);
        this.transform.localScale = new Vector2(finalScale, finalScale); 

        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _rb.linearVelocityY = -(data.MinFallSpeed + (data.GetFallSpeedSpread() * proportion));

        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
        _collider.size = data.ColliderSize;
    }
}
