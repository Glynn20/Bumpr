using System;
using TMPro;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private GameObject _transitionObject;
    [SerializeField] private TextMeshProUGUI _sceneNumberText;
    [SerializeField] private TextMeshProUGUI _sceneDescriptionText;
    [SerializeField] private string _transitionStartAnimName;
    [SerializeField] private string _transitionEndAnimName;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;

    [Header("SFX")] 
    [SerializeField] private SoundData _sceneTransitionInFX;
    
    private Animator _transitionObjectAnimator;

    public void OnLevelBegin(Component sender, params object[] data)
    {
        if (data.Length > 0 && data[0] is SceneData currentScene)
        {
            PlayTransitionEndAnimation(currentScene);
            UpdateSceneText(currentScene);
        } 
    }

    public void OnNextLevelButtonPressed()
    {
        PlayTransitionStartAnimation();
    }

    private void Awake()
    {
        if (_transitionObject == null) return;
        _transitionObjectAnimator = _transitionObject.GetComponent<Animator>(); 
    }
    public void PlayTransitionEndAnimation(SceneData sceneData)
    {
        if (_transitionObjectAnimator == null) return;
        
        UpdateSceneText(sceneData);
        _transitionObjectAnimator.Play(_transitionEndAnimName);
    }

    public void PlayTransitionStartAnimation()
    {
        if (_transitionObjectAnimator == null) return;
        OnPlaySound?.Raise(this, _sceneTransitionInFX);
        _transitionObjectAnimator.Play(_transitionStartAnimName);
    }

    private void UpdateSceneText(SceneData data)
    {
        if (_sceneNumberText == null || _sceneDescriptionText == null) return;
        _sceneDescriptionText.text = data.InGameDisplayName;
        _sceneNumberText.text = data.SceneName;
    }
}
