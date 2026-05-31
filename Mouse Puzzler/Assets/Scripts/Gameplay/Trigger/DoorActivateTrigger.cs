using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DoorActivateTrigger : ActivatableTrigger
{
    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;
    
    [Header("Sound")]
    [SerializeField] private SoundData _onTriggerSoundData;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.GetComponent<IPlayer>() != null)
        {
            OnPlaySound?.Raise(this, _onTriggerSoundData);
        }
    }
}
