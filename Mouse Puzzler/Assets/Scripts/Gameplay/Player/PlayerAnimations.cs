using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private PlayerMovement _movement;

    public void TriggerDeath() // Called as GameEvent response via inspector
    {
        Debug.Log("Trigger death called");
        _animator.SetTrigger("Died");
        //_animator.ResetTrigger("Died");
    }

    public void ResetToEnterState()
    {
        _animator.Play("Idle");
    } 

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_movement == null) _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _movement.GetCurrentSpeed());
    }
}
