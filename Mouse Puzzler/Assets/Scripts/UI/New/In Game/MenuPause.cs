using UnityEngine;

public class MenuPause : Menu<MenuPause>
{
    [Header("Child Config")]

    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;
    [SerializeField] private GameEvent OnPausePressed;

    [Header("Data")]
    [SerializeField] private SceneData _mainMenuSceneData;

    public void OnPauseStateChanged(Component sender, params object[] data)
    {
        if (data.Length == 1 && data[0] is bool isNowPaused)
        {
            if (isNowPaused)
            {
                Show();
            }
        }
    }

    public override void OnBackPressed()
    {
        Hide();
    }

    public void ButtonPressedResume()
    {
        OnPausePressed?.Raise(this, null);
        Hide();
    }
    public void ButtonPressedReturnToMainMenu()
    {
        OnRequestLoadScene?.Raise(this, _mainMenuSceneData);
    }
    public void ButtonPressedSettings()
    {
        MenuSettings.Show();
    }
}
