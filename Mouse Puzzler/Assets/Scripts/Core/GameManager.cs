using UnityEditor;
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
    public void OnLevelBegin()
    {
        _gamePaused = false;
        Time.timeScale = 1.0f;
        PauseStateChanged?.Raise(this, _gamePaused);
    }
}
