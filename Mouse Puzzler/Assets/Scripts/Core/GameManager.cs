using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public bool GamePaused => _gamePaused;
    private bool _gamePaused = false;
    private static GameManager _instance;
    

    [Header("Events")]
    [SerializeField] private GameEvent PauseStateChanged;

    public void OnPausePressed()
    {
        _gamePaused = !_gamePaused;

        Time.timeScale = _gamePaused ? 0.0f : 1.0f;
        PauseStateChanged?.Raise(this, _gamePaused);
    }

    public void OnLevelLoadComplete(Component sender, params object[] data)
    {
        if (data != null && 
        data.Length == 1 &&
        data[0] is SceneData sceneData)
        {
            _gamePaused = false;
            Time.timeScale = 1.0f;
            PauseStateChanged?.Raise(this, _gamePaused);
        }
    }
    public void OnLevelBegin()
    {
        
    }

    public void OnRequestScreenModeChange(Component sender, params object[] data)
    {
        if (data.Length > 0 && data[0] is FullScreenMode mode)
        {
            GameSettings.SetScreenMode(mode);
        }
    }

}
