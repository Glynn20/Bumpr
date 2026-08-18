using UnityEngine;

public class BasicEventTriggerHandler : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private GameEvent OnAnimationEvent;

    public void RaiseAnimationEvent()
    {
        OnAnimationEvent?.Raise(this, null);
    }
}
