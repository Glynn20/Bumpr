using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    private Canvas _canvas;

    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadNextLevel;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public void OnLevelComplete(Component sender, params object[] data)
    {
        _canvas.enabled = true;
    }
    public void OnNextLevelButtonClicked()
    {
        _canvas.enabled = false;
        OnRequestLoadNextLevel?.Raise(this, null);
    }
}
