using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour, IMenu
{
    private Canvas _canvas;

    [Header("Events")]
    [SerializeField] private GameEvent OnInGameMenuOpen;
    [SerializeField] private GameEvent OnInGameMenuClose;

    public void HandlePauseStateChanged(Component sender, params object[] data)
    {
        if (data.Length == 1 && data[0] is bool isNowPaused)
        {
            if (isNowPaused)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();                
            }
        }
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
    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        CloseMenu();
    }
}
