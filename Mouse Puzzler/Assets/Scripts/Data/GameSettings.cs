using Unity.VisualScripting;
using UnityEngine;


public static class GameSettings 
{
    // KEYS
    public const string KEY_VOLUME = "MasterVolume";
    public const string KEY_SENSITIVITY = "MouseSensitivity";
    public const string KEY_SCREENMODE = "ScreenMode";

    // VALUES
    public const float DEFAULT_VOLUME = 1.0f;
    public const float DEFAULT_SENSITIVITY = 0.25f;
    public const int DEFAULT_SCREENMODE = 0;

    // Done initially. Does not save settings
    public static void SetScreenMode(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }
    
    public static void SetScreenMode(FullScreenMode mode)
    {
        Screen.fullScreenMode = mode;
        UpdateScreenSettings(mode);
        Debug.Log(Screen.fullScreenMode);
    }

    private static void UpdateScreenSettings(FullScreenMode mode)
    {
        int newInt = 0;
        if (mode == FullScreenMode.FullScreenWindow)
        {
            newInt = 1;
        }
        PlayerPrefs.SetInt(GameSettings.KEY_SCREENMODE, newInt);
        PlayerPrefs.Save();
    }
}

