using UnityEngine;

public class SettingsInitializer : MonoBehaviour
{

    // Refer to GameSettings for all Keys and Values
    [SerializeField] private GameEvent OnRequestSetMasterVolume;


    private void Start()
    {
        // Adjust as needed (all GameSetting keys)
        CheckForKey(GameSettings.KEY_SENSITIVITY, GameSettings.DEFAULT_SENSITIVITY);
        CheckForKey(GameSettings.KEY_VOLUME, GameSettings.DEFAULT_VOLUME);

        PlayerPrefs.Save();
        float volume = PlayerPrefs.GetFloat(GameSettings.KEY_VOLUME, GameSettings.DEFAULT_VOLUME);
        OnRequestSetMasterVolume?.Raise(this, volume);
    }

    private void CheckForKey(string key, string value)
    {
       if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, value);
        }
    }

    private void CheckForKey(string key, float value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }

}
