using UnityEngine;

public class UIButtonAudio : MonoBehaviour
{
    [Header("SFX")] 
    [SerializeField] private SoundData UIClickSound;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;
    public void PlayAudio()
    {
        OnPlaySound?.Raise(this, UIClickSound);
    }
}
