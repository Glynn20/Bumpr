using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteMenu : MonoBehaviour, IMenu
{
    private Canvas _canvas;

    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadNextLevel;
    [SerializeField] private GameEvent OnMenuOpen;
    [SerializeField] private GameEvent OnMenuClose;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        CloseMenu();
    }

    public void OpenMenu()
    {
        OnMenuOpen?.Raise(this, null);
        _canvas.enabled = true;
    }

    public void CloseMenu()
    {
        OnMenuClose?.Raise(this, null);
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
