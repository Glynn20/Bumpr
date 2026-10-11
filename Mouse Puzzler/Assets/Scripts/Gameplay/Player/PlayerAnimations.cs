using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private SoundData _playerDeathSoundData;
    [SerializeField] private ParticleData _playerDeathFX;
    [SerializeField] private GameEvent OnPlayPlayerDeathSound;
    [SerializeField] private GameEvent OnPlayPlayerDeathParticle;

    public void OnLevelComplete()
    {
        PlayCelebration();
    }

    public void TriggerDeath() // Called as GameEvent response via inspector
    {
        _animator.SetTrigger("Died"); // Plays Death Anim
        OnPlayPlayerDeathSound?.Raise(this, _playerDeathSoundData);
        OnPlayPlayerDeathParticle?.Raise(this, _playerDeathFX, new Vector2(_transform.position.x, _transform.position.y));
    }

    public void ResetToEnterState()
    {
        _animator.Play("Idle");
    } 

    private void Awake()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();
        if (_movement == null) _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _movement.GetCurrentSpeed());
    }

    public void PlayCelebration()
    {
        _animator.Play("Celebrate");
    }
}
