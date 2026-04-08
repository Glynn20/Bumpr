using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColour : MonoBehaviour, ICanChangeColour
{
    [SerializeField] private GameEvent OnPlayerChangeColour;
    public ColourMode CurrentColour => _currentColour;
    private ColourMode _currentColour = ColourMode.None;
    private SpriteRenderer _sprite;

    public void ChangeColour(ColourMode newColour)
    {
        if (_currentColour == newColour) return;
        
        _currentColour = newColour;

        switch (newColour)
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

        OnPlayerChangeColour?.Raise(this, newColour);
    }
    
    void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        
        if (_sprite == null)
        {
            Debug.Log("Player Sprite Renderer not set.", this);
            return;
        }
    }
}
