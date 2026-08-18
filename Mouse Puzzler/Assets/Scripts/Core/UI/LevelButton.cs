using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [HideInInspector] public SceneData SceneOnClick;
    [HideInInspector] public TextMeshProUGUI Text;
    
    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;

    [Header("Config")]
    [SerializeField] private GameObject _lockedIcon;

    private Image _image;
    
    public void Initialize(SceneData scene)
    {
        SceneOnClick = scene;
        Text.text = scene.SceneName;
        SetColor(scene);
    }

    public void LoadLevel()
    {
        OnRequestLoadScene?.Raise(this, SceneOnClick);
    }
    private void Awake()
    {
        Text = GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponent<Image>();
    }

    private void SetColor(SceneData scene)
    {
        if (_image == null) return;

        switch (scene.levelType)
        {
            case (SceneData.LevelType.Normal):
                _image.color = scene.ColourData.unlockedNormalColour;
            break;
            case (SceneData.LevelType.Challenge):
                _image.color = scene.ColourData.unlockedChallengeColour;
            break;
            case (SceneData.LevelType.Nightmare):
                _image.color = scene.ColourData.unlockedNightmareColour;
            break;
        }
    }

    private void Lock()
    {
        _lockedIcon.SetActive(true);
    }

    private void Unlock()
    {
        _lockedIcon.SetActive(false);
    }

}
