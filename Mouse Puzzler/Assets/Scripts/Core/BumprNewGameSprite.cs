using UnityEngine;

public class BumprNewGameSprite : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent OnMouseTouchBumprSprite;
    [SerializeField] private GameEvent OnPlaySound;

    [Header("Animation")]
    [SerializeField] private ParticleSystem[] _onTouchFX;
    [SerializeField] private string _reviveAnimName;
    [SerializeField] private string _fallAnimName;

    [Header("Sounds")]
    [SerializeField] private SoundData _bumprJumpUpSound;
    [SerializeField] private SoundData _bumprJumpDownSound;
    [SerializeField] private SoundData _bumprFallSound;
    [SerializeField] private SoundData _bumprThudSound;
    private Animator _animator;

    public void PlayReviveAnim()
    {
        _animator?.Play(_reviveAnimName);
    }
    public void PlayFallAnim()
    {
        _animator.Play(_fallAnimName);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            OnMouseTouchBumprSprite?.Raise(this, null);
            foreach(ParticleSystem fx in _onTouchFX)
            {
                fx.Play();   
            }
            PlayReviveAnim();
        }
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    // Anim Keyframe Sounds

    public void PlayJumpUpSound()
    {
        OnPlaySound?.Raise(this, _bumprJumpUpSound);
    }

    public void PlayJumpDownSound()
    {
        OnPlaySound?.Raise(this, _bumprJumpDownSound);
    }
    public void PlayFallSound()
    {
        OnPlaySound?.Raise(this, _bumprFallSound);
    }    
    public void PlayThudSound()
    {
        OnPlaySound?.Raise(this, _bumprThudSound);
    }
}
