using UnityEngine;

public class MenuGameOver : Menu<MenuGameOver>
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
