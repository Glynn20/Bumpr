using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class NewGameHandler : MonoBehaviour
{
    [Header("Animation names")]
    [SerializeField] private string _newGameAnimName;
    [SerializeField] private string _bumpRFallSequenceAnimName;
    [SerializeField] private string _bumprReviveEffectAnimName;
    [Header("Other")]
    [SerializeField] private GameObject _dummyCursor;
    [SerializeField] private GameObject _bumprModel;
    [SerializeField] private SceneData _sceneToLoad;
    [SerializeField] private SoundData _wakeUpTextUISound;
    [SerializeField] private SoundData _bumprReviveExplosionSound;

    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;
    [SerializeField] private GameEvent OnPlaySound;
    private BumprNewGameSprite _bumpr;
    private Animator _animator;
    private Coroutine _bumprReviveCoroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _bumpr = _bumprModel.GetComponent<BumprNewGameSprite>();
    }

    // Sequence of Events (in order)
    public void BeginNewGame()
    {
        _animator.Play(_newGameAnimName);
    }

    public void PlayBumprFallSequence()
    {
        _animator.Play(_bumpRFallSequenceAnimName);
        _bumpr.PlayFallAnim();
    }
    public void PlayBumperReviveSequence()
    {
        OnPlaySound?.Raise(this, _bumprReviveExplosionSound);
        _animator.Play(_bumprReviveEffectAnimName);
        DisableDummyCursor();
    }


    // Animation Keyframe Events
    public void WakeUpTextShown()
    {
        OnPlaySound?.Raise(this, _wakeUpTextUISound);
    }
    public void EnableDummyCursor()
    {
        _dummyCursor?.SetActive(true);
    }
    public void DisableDummyCursor()
    {
        _dummyCursor?.SetActive(false);
    }
    public void LoadNextScene()
    {
        OnRequestLoadScene?.Raise(this, _sceneToLoad);
    }
    public void OnMouseTouchBumprModel()
    {
        PlayBumperReviveSequence();
    }

}
