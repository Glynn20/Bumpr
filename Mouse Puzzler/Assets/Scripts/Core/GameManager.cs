using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _gamePaused = false;
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [Header("Events")]
    [SerializeField] private GameEvent PauseStateChanged;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (_instance != this)
        {
            Destroy(this);
        }
    }

    public void OnPausePressed()
    {
        if (_gamePaused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        _gamePaused = true;
        PauseStateChanged?.Raise(this, true);
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
        _gamePaused = false;
        PauseStateChanged?.Raise(this, false);
    }
}
