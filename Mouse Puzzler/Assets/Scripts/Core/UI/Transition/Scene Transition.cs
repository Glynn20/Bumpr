using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private GameObject _transitionObject;
    [SerializeField] private string _transitionStartAnimName;
    [SerializeField] private string _transitionEndAnimName;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;

    [Header("SFX")] 
    [SerializeField] private SoundData _sceneTransitionInFX;
    
    private Animator _transitionObjectAnimator;

    public void OnLevelBegin()
    {
        PlayTransitionEndAnimation();
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
    public void PlayTransitionEndAnimation()
    {
        if (_transitionObjectAnimator == null) return;
        _transitionObjectAnimator.Play(_transitionEndAnimName);
    }

    public void PlayTransitionStartAnimation()
    {
        if (_transitionObjectAnimator == null) return;
        OnPlaySound?.Raise(this, _sceneTransitionInFX);
        _transitionObjectAnimator.Play(_transitionStartAnimName);
    }
}
