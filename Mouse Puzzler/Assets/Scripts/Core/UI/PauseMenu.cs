using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour, IMenu
{
    [SerializeField] private GameEvent OnMenuOpen;
    [SerializeField] private GameEvent OnMenuClose;
    private Canvas _canvas;

    public void OnPauseStateChanged(Component sender, params object[] data)
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
        if (_canvas.enabled) return;

        OnMenuOpen?.Raise(this, null);
        _canvas.enabled = true;
    }

    public void CloseMenu()
    {
        if (!_canvas.enabled) return;

        OnMenuClose?.Raise(this, null);
        _canvas.enabled = false;
    }

    public void OnDisable()
    {
        CloseMenu();
    }

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        CloseMenu();
    }
}
