using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Menu<T> : Menu where T : Menu<T>
{
    public static T Instance {get; private set;}

    [Header("SFX")]   
    [SerializeField] protected SoundData _UIButtonSound;
    [SerializeField] protected GameEvent OnPlaySound;


    protected override void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
            return;
        }

        Instance = this as T;
        base.Awake();
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public static void Show()
    {
        if (Instance != null)
        {
            MenuManager.Instance.OpenMenu(Instance);
        }
    }
    
    public static void Hide()
    {
        if (Instance != null)
        {
            MenuManager.Instance.CloseMenu(Instance);
        }
    }

    protected void PlayUISound()
    {
        OnPlaySound?.Raise(this, _UIButtonSound);
    }

}
