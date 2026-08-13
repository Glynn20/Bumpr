using UnityEngine;

public class MenuInGameSettings : Menu<MenuInGameSettings>
{
    public override void OnBackPressed()
    {
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
}
