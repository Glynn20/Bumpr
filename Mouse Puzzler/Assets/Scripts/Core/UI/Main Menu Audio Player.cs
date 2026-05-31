using UnityEngine;

public class MainMenuAudioPlayer : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlaySoundMainMenu;
    [SerializeField] private SoundData _mainMenuSoundData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _onPlaySoundMainMenu?.Raise(this, _mainMenuSoundData);
    }

}
