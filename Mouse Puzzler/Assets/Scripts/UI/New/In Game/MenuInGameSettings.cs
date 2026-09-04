using UnityEngine;
using UnityEngine.UI;
public class MenuInGameSettings : Menu<MenuInGameSettings>
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;

    public override void OnBackPressed()
    {
        Hide();
    }

    public override void OnOpen()
    {
        base.OnOpen();

        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat(GameSettings.KEY_VOLUME, GameSettings.DEFAULT_VOLUME);
        }
        else
        {
            Debug.LogWarning("Volume Slider reference not set in inspector", this);
        }

        if (sensitivitySlider != null)
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat(GameSettings.KEY_SENSITIVITY, GameSettings.DEFAULT_SENSITIVITY);        }
        else
        {
            Debug.LogWarning("Sensitivity Slider reference not set in inspector", this);
        }
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
