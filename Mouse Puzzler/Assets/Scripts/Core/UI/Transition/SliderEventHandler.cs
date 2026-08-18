using UnityEngine;

public class SliderEventHandler : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private GameEvent Event;

    public void RaiseAnimationEvent(float sliderValue)
    {
        Event?.Raise(this, sliderValue);
    }
}
