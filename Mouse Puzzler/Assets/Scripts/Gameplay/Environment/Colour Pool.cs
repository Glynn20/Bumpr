using UnityEngine;

public class ColourPool : MonoBehaviour
{
    [SerializeField] ColourMode _currentColour;
    private SpriteRenderer _sprite;
    public void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.TryGetComponent<ICanChangeColour>(out var colourChanger))
        {
            colourChanger?.ChangeColour(_currentColour);     
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
