using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [HideInInspector] public SceneData SceneOnClick;
    [SerializeField] private TextMeshProUGUI LevelName;
    [SerializeField] private TextMeshProUGUI LevelDisplayText;
    
    
    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;

    [Header("Config")]
    //[SerializeField] private GameObject _lockedIcon;

    [SerializeField] private TextMeshProUGUI _sTime;
    [SerializeField] private TextMeshProUGUI _aTime;
    [SerializeField] private TextMeshProUGUI _bTime;

    private Image _backgroundImage;
    
    public void Initialize(SceneData scene)
    {
        SceneOnClick = scene;
        LevelName.text = scene.SceneName;
        LevelDisplayText.text = scene.InGameDisplayName;
        SetColor(scene);

        if (_sTime != null)
        {
            _sTime.text = ConvertToClockTime(scene.STime);
        }
        if (_aTime != null)
        {
            _aTime.text = ConvertToClockTime(scene.ATime);
        }
        if (_bTime != null)
        {
            _bTime.text = ConvertToClockTime(scene.BTime);
        }
    }

    public void LoadLevel()
    {
        OnRequestLoadScene?.Raise(this, SceneOnClick);
    }
    private void Awake()
    {
        _backgroundImage = GetComponent<Image>();
    }

    private void SetColor(SceneData scene)
    {
        if (_backgroundImage == null) return;

        switch (scene.levelType)
        {
            case (SceneData.LevelType.Normal):
                _backgroundImage.color = scene.ColourData.unlockedNormalColour;
            break;
            case (SceneData.LevelType.Challenge):
                _backgroundImage.color = scene.ColourData.unlockedChallengeColour;
            break;
            case (SceneData.LevelType.Nightmare):
                _backgroundImage.color = scene.ColourData.unlockedNightmareColour;
            break;
        }
    }

    private string ConvertToClockTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return $"{minutes:D2}:{seconds:D2}";
    }

}
