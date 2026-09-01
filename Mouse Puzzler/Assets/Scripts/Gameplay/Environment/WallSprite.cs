using UnityEngine;

public class WallSprite : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer parentWallRenderer;
    private SpriteRenderer _renderer;
    private void Start()
    {
        if (parentWallRenderer == null) return;
        _renderer = GetComponent<SpriteRenderer>();

        _renderer.size = new Vector2(parentWallRenderer.size.x, parentWallRenderer.size.y);
    }
}
