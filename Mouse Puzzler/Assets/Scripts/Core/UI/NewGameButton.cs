using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private GameEvent OnNewGamePressed;
    [SerializeField] private GameEvent OnPlaySound;

    [Header("SFX")] 
    [SerializeField] private SoundData NewGameSound;
    public void RaiseNewGameEvent()
    {
        OnPlaySound?.Raise(this, NewGameSound);
        OnNewGamePressed?.Raise(this, null);
    }
}
