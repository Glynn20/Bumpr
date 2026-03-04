using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteMenu : MonoBehaviour, IMenu
{
    private Canvas _canvas;

    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadNextLevel;
    [SerializeField] private GameEvent OnInGameMenuOpen;
    [SerializeField] private GameEvent OnInGameMenuClose;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        CloseMenu();
    }

    public void OpenMenu()
    {
        OnInGameMenuOpen?.Raise(this, null);
        _canvas.enabled = true;
    }

    public void CloseMenu()
    {
        OnInGameMenuClose?.Raise(this, null);
        _canvas.enabled = false;
    }

    public void OnLevelComplete(Component sender, params object[] data)
    {
        OpenMenu();
    }
    public void OnNextLevelButtonClicked()
    {
        CloseMenu();
        OnRequestLoadNextLevel?.Raise(this, null);
    }

    
}
