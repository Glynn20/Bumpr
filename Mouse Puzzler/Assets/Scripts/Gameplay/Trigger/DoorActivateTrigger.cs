using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DoorActivateTrigger : ActivatableTrigger, IReset
{
    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;
    
    [Header("Sound")]
    [SerializeField] private SoundData _onTriggerSoundData;

    [Header("Sprites")]
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _pressedSprite;
    private SpriteRenderer _renderer;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.GetComponent<IPlayer>() != null)
        {
            OnPlaySound?.Raise(this, _onTriggerSoundData);
            _renderer.sprite = _pressedSprite;
        }
    }

    public override void Reset()
    {
        base.Reset();
        _renderer.sprite = _normalSprite;
    }

    protected override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _normalSprite;
    }

}
