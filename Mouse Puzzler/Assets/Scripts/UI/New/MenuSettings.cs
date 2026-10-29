using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : Menu<MenuSettings>
{
    [Header("UI")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;

    [Header("Events")]
    [SerializeField] private GameEvent OnRequestSetFullScreenMode;
    [SerializeField] private GameEvent OnRequestSetMasterVolume;
    [SerializeField] private GameEvent OnSensitivityChanged; // for player to change at runtime
    
    public override void OnBackPressed()
    {
        PlayUISound();
        Hide();
    }
    public override void OnOpen()
    {
        base.OnOpen();

        // Make slider match current settings visually
        if (volumeSlider != null && volumeText != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat(GameSettings.KEY_VOLUME, GameSettings.DEFAULT_VOLUME);
            SetVolumeText(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("Volume Slider reference not set in inspector", this);
        }

        if (sensitivitySlider != null && sensitivityText != null)
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat(GameSettings.KEY_SENSITIVITY, GameSettings.DEFAULT_SENSITIVITY);
            SetSensitivityText(sensitivitySlider.value);
        }
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

    public void OnNewGamePressed()
    {
        Hide();
    }
    
    public void OnFullscreenButtonToggled(bool isOn)
    {
        if (isOn)
        {
            OnRequestSetFullScreenMode?.Raise(this, FullScreenMode.ExclusiveFullScreen);
        }
        else
        {
            OnRequestSetFullScreenMode?.Raise(this, FullScreenMode.FullScreenWindow);
        }
    }

    public void OnVolumeSliderValueChanged(float newValue)
    {
        OnRequestSetMasterVolume?.Raise(this, newValue);
        SetVolumeText(newValue); 
    }
    
    public void OnSensitivitySliderValueChanged(float newValue)
    {
        PlayerPrefs.SetFloat(GameSettings.KEY_SENSITIVITY, newValue);
        PlayerPrefs.Save();

        SetSensitivityText(newValue);
        OnSensitivityChanged?.Raise(this, newValue);
    }

    private void SetVolumeText(float value)
    {
        volumeText.text = $"GAME VOLUME: {Math.Round(value, 1)}";
    }
    private void SetSensitivityText(float value)
    {
        sensitivityText.text = $"SENSITIVITY: {Math.Round(value, 2)}";
    }
    
}
