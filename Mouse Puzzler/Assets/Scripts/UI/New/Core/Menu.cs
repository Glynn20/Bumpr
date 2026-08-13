using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Menu : MonoBehaviour
{
    [Header("Config")]
    public bool hideUnderlyingMenus = true;
    public bool IsPartOfPauseMenu = false;

    [Header("Events")]
    [SerializeField] private GameEvent OnMenuOpen;
    [SerializeField] private GameEvent OnMenuClose;
    protected CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        SetVisible(false);
        SetInteractable(false);
    }

    public virtual void OnOpen()
    {
        OnMenuOpen?.Raise(this, null);
        
        SetInteractable(true);
        SetVisible(true);
    }

    public virtual void OnClose()
    {
        OnMenuClose?.Raise(this, null);

        SetInteractable(false);
        SetVisible(false);
    }

    public virtual void OnPause()
    {
        SetInteractable(false);
        if (hideUnderlyingMenus)
        {
            SetVisible(false);
        }
    }

    public virtual void OnResume()
    {
        SetInteractable(true);
        SetVisible(true);
    }

    public void SetInteractable(bool state)
    {
        if (_canvasGroup == null) return;
        
        _canvasGroup.interactable = state;
        _canvasGroup.blocksRaycasts = state;
        
    }

    public void SetVisible(bool state)
    {
        if (_canvasGroup == null) return;

        _canvasGroup.alpha = state == true ? 1 : 0;
    }
    public abstract void OnBackPressed();
    
    
}
