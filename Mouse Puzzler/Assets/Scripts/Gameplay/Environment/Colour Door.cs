using System.Runtime.CompilerServices;
using UnityEngine;

public class ColourDoor : MonoBehaviour
{
    public ColourMode CurrentColour => _currentColour;

    [Header("Colour")]
    [SerializeField] private ColourMode _currentColour;
    
    private SpriteRenderer _sprite;
    private Collider2D _collider;

    public void OnPlayerChangeColour(Component sender, params object[] data)
    {
        if (data != null &&
            data[0] is ColourMode newPlayerColour)
        {
            if (_currentColour == newPlayerColour) _collider.enabled = false;
            else _collider.enabled = true;
        }
        else
        {
            Debug.Log("OnPlayerChangeColour event needs to pass in ColourMode newColour", this);
            return;
        }
    }

    void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        
        if (_sprite == null)
        {
            Debug.Log("Door Sprite Renderer not set.", this);
            return;
        }

        switch (_currentColour)
        {
            case ColourMode.Red:
            _sprite.color = Color.red; break;
            case ColourMode.Green:
            _sprite.color = Color.green; break;
            case ColourMode.Blue:
            _sprite.color = Color.blue; break;
            case ColourMode.None:
            _sprite.color = Color.white; break;
        }
    }
}
