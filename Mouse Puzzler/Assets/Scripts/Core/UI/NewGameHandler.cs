using NUnit.Framework;
using UnityEngine;

public class NewGameHandler : MonoBehaviour
{
    [SerializeField] private string _newGameAnimName;
    [SerializeField] private string _bumpRSequenceAnimName;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }
    public void BeginNewGame()
    {
        _animator.Play(_newGameAnimName);
    }

    public void PlayBumprInitialSequence()
    {
        _animator.Play(_bumpRSequenceAnimName);
    }
}
