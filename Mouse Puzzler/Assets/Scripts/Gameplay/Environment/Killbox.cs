using System.Security;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] private GameObject _shadow;
    SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        if (_shadow != null)
        {
            SpriteRenderer shadowSprite = _shadow.GetComponent<SpriteRenderer>(); 
            shadowSprite.size = new Vector2(_renderer.size.x, _renderer.size.y); 
        }
    }
}
