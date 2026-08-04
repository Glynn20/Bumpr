using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private GameEvent OnAnimationEvent;

    public void RaiseAnimationEvent()
    {
        OnAnimationEvent?.Raise(this, null);
    }
}
