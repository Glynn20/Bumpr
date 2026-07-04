using System.Security;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] private GameObject _shadow;
    [SerializeField] private BoxCollider2D _killboxCollider;
    [SerializeField] private float _colliderLeniency = 0.05f;
    SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        if (_shadow != null)
        {
            SpriteRenderer shadowSprite = _shadow.GetComponent<SpriteRenderer>(); 
            shadowSprite.size = new Vector2(_renderer.size.x, _renderer.size.y); 
        }

        _killboxCollider.size = new Vector2(
            Mathf.Clamp(_renderer.size.x - _colliderLeniency, 0.01f, 100f), 
            Mathf.Clamp(_renderer.size.y - _colliderLeniency, 0.01f, 100f)
            );
    }
}
