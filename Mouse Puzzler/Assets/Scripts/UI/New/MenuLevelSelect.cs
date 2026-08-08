using UnityEngine;

public class MenuLevelSelect : Menu<MenuLevelSelect>
{
    public override void OnBackPressed()
    {
        PlayUISound();
        Hide();
    }

    public void OnContinueButtonClicked()
    {
        Hide();
    }
}
