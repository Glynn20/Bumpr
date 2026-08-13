using UnityEngine;

public class MenuSettings : Menu<MenuSettings>
{
    public override void OnBackPressed()
    {
        PlayUISound();
        Hide();
    }

    public void OnPlayerDeath()
    {
        Show();
    }

    public void OnContinueButtonClicked()
    {
        Hide();
    }

    public void OnNewGamePressed()
    {
        Hide();
    }
}
