using UnityEngine;

public class BumprNewGameSprite : MonoBehaviour
{
    [SerializeField] private GameEvent OnMouseTouchBumprSprite;
    [SerializeField] private ParticleSystem[] _onTouchFX;
    [SerializeField] private string _reviveAnimName;
    [SerializeField] private string _fallAnimName;
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
     
}
