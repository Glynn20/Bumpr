using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour, IMenu
{
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
        _canvas.enabled = true;
    }

    public void CloseMenu()
    {
        _canvas.enabled = false;
    }
    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        CloseMenu();
    }
}
