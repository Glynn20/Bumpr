using UnityEngine;

public class EventCallButton : MonoBehaviour
{
    [Header("SFX")] 
    [SerializeField] private SoundData UIClickSound;

    [Header("Events")]
    [SerializeField] private GameEvent OnClickEvent;
    [SerializeField] private GameEvent OnPlaySound;
    public void OnClick()
    {
        OnClickEvent?.Raise(this, null);
        OnPlaySound?.Raise(this, UIClickSound);
    }

}
