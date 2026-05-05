using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour, IMenu
{
    private Canvas _canvas;

    [Header("Event")]
    [SerializeField] private GameEvent _onContinue; 
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
    
    public void OnContinueButtonClicked()
    {
        _onContinue?.Raise(this, null);
        CloseMenu();
    }

    public void OnPlayerDeath()
    {
        OpenMenu();
    }

}
