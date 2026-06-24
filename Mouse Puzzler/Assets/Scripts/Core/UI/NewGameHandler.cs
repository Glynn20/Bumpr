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
    [Header("Events")]
    [SerializeField] private GameEvent OnRequestLoadScene;
    private BumprNewGameSprite _bumpr;
    private Animator _animator;
    private Coroutine _bumprReviveCoroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _bumpr = _bumprModel.GetComponent<BumprNewGameSprite>();
    }
    public void BeginNewGame()
    {
        _animator.Play(_newGameAnimName);
    }

    public void PlayBumprFallSequence()
    {
        _animator.Play(_bumpRFallSequenceAnimName);
        _bumpr.PlayFallAnim();
    }

    public void EnableDummyCursor()
    {
        _dummyCursor?.SetActive(true);
    }
    public void OnMouseTouchBumprModel()
    {
        if (_bumprReviveCoroutine == null)
        {
            _bumprReviveCoroutine = StartCoroutine(BumprReviveCoroutine());
        }
    }

    private IEnumerator BumprReviveCoroutine(float timeBeforeSceneSwitch = 2f)
    {
        Debug.Log("Next Scene Requested", this);
        _animator.Play(_bumprReviveEffectAnimName);
        yield return new WaitForSeconds(timeBeforeSceneSwitch);
        _bumprReviveCoroutine = null;
        OnRequestLoadScene?.Raise(this, _sceneToLoad);
        
    }
}
