using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private Canvas _canvas;

    [Header("Event")]
    [SerializeField] private GameEvent _onContinue; 

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }
    public void OnContinueButtonClicked()
    {
        _onContinue?.Raise(this, null);
        DisableCanvas();
    }

    public void OnPlayerDeath()
    {
        EnableCanvas();
    }

    void EnableCanvas()
    {
        Debug.Log("Canvas enabled");
        _canvas.enabled = true;
    }

    void DisableCanvas()
    {
        _canvas.enabled = false;
    }

}
