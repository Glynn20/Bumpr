using UnityEngine;

public class MenuGameOver : Menu<MenuGameOver>
{

    [SerializeField] private GameEvent OnPlayerContinue;
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
        PlayUISound();
        OnPlayerContinue?.Raise(this, null);
        Hide();
    }
}
