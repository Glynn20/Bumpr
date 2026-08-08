using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MenuMainMenuBase : Menu<MenuMainMenuBase>
{
    [SerializeField] private TilemapRenderer[] _tileRenderers;

    [Header("Events")]
    [SerializeField] private GameEvent OnNewGamePressed;
    
    public override void OnBackPressed(){}

    public override void OnOpen()
    {
        EnableRenderers();
        base.OnOpen();
    }

    public override void OnClose()
    {
        DisableRenderers();
        base.OnClose();
    }

    public override void OnResume()
    {
        EnableRenderers();
        base.OnResume();
    }

    public override void OnPause()
    {
        DisableRenderers();
        base.OnPause();
    }
    public void OnContinueButtonClicked()
    {
        PlayUISound();
        Hide();
    }

    public void NewGameButtonPressed()
    {
        OnNewGamePressed?.Raise(this, null);
    }

    public void LevelSelectButtonPressed()
    {
        PlayUISound();
        MenuLevelSelect.Show();
        //
    }

    public void SettingsButtonPressed()
    {
        PlayUISound();
        MenuSettings.Show();
        //
    }

    public void QuitGameButtonPressed()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    
    private void DisableRenderers()
    {
        foreach (TilemapRenderer r in _tileRenderers)
        {
            r.enabled = false;
        }
    }
    private void EnableRenderers()
    {
        foreach (TilemapRenderer r in _tileRenderers)
        {
            r.enabled = true;
        }
    }
}
