using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
public enum PlayerMoveState
{
    Idle = 0,
    Walking = 1,
    Running = 2,
    Overheating = 3
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IMovement
{
    public float GetCurrentSpeed() => _currentSpeed;
    [SerializeField] protected float _mouseSens; 
    [SerializeField] private PlayerMovementConfig _config;
    [SerializeField] private bool _flipSprite;
    private float _minMouseSens = 0.01f;
    private float _maxMouseSens = 1.0f;
    private Rigidbody2D _rb;
    private UnityEngine.Vector2 _accumulatedInput;
    private UnityEngine.Vector2 _direction;
    private Transform _transform;
    private bool _isInputEnabled = true;
    private bool _stoppedMoving;
    private float _currentSpeed; 
    private UnityEngine.Vector2 _lastPosition;
    private SpriteRenderer _sprite;

    [Header("Sound")]
    [SerializeField] private SoundData _startMovingSoundData;
    [SerializeField] private ParticleData _startMovingParticle;

    [Header("Events")]
    [SerializeField] private GameEvent OnPlaySound;
    [SerializeField] private GameEvent OnPlayParticleEffect;

    public void ReadMouseInput(InputAction.CallbackContext context)
    {
        if (_isInputEnabled == false) return;
        _accumulatedInput += context.ReadValue<UnityEngine.Vector2>();
    }

    public void DisableInput()
    {
        _isInputEnabled = false;
        _currentSpeed = 0;
        ResetAccumulatedInputs();
    }

    public void OnLevelBegin()
    {
        EnableInput();
    }
    
    public void EnableInput()
    {
        _isInputEnabled = true;
        ResetAccumulatedInputs();
    }

    public void SetInputEnabled(bool state)
    {
        _isInputEnabled = state;
        ResetAccumulatedInputs();    
    }
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;   
    }
    public PlayerMoveState GetMoveState()
    {
        if (_currentSpeed > _config.RunToOverheatThreshold)
        {
            return PlayerMoveState.Overheating;
        }
        else if (_currentSpeed > _config.WalkToRunTransitionThreshold)
        {
            return PlayerMoveState.Running;
        }
        else if (_currentSpeed > _config.IdleToWalkTransitionThreshold)
        {
            return PlayerMoveState.Walking;
        }
        return PlayerMoveState.Idle;  
    }
    public void OnPauseStateChanged(Component sender, params object[] data)
    {
        if (data.Length == 1 && data[0] is bool isNowPaused)
        {
            if (isNowPaused)
            {
                DisableInput();
            }
            else
            {
                EnableInput();
            }
        }
    }

    public virtual void FixedUpdate()
    {
        if (_isInputEnabled == false) return;
        AdjustPosition();
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();  
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _transform = transform;
        _lastPosition = _rb.position;

        HideCursor();
        _mouseSens = Mathf.Clamp(_mouseSens, _minMouseSens, _maxMouseSens);
    }    

    void AdjustPosition()
    {
        float actualDistanceMoved = UnityEngine.Vector2.Distance(_rb.position, _lastPosition);
        _currentSpeed = actualDistanceMoved / Time.fixedDeltaTime;

        if (_stoppedMoving)
        {
            if (actualDistanceMoved > _config.minDistanceToPlaySound) 
            {
                OnPlaySound?.Raise(this, _startMovingSoundData);
                OnPlayParticleEffect?.Raise(this, _startMovingParticle, new UnityEngine.Vector2(_transform.position.x, _transform.position.y));
                _stoppedMoving = false;
            }
        }
            
        if (_accumulatedInput != UnityEngine.Vector2.zero)
        {
            UnityEngine.Vector2 moveAmount = _accumulatedInput * _mouseSens * Time.fixedDeltaTime;
            UnityEngine.Vector2 targetPosition = _rb.position + moveAmount;
           
            _rb.MovePosition(targetPosition);
            _direction = GetDirection(transform.position, targetPosition);
            
            if (_flipSprite) _sprite.flipX = _direction.x < 0;


            ResetAccumulatedInputs();
        }
        else
        {
            _rb.linearVelocity = UnityEngine.Vector2.zero;
            _stoppedMoving = true;
            _currentSpeed = 0;
        }

        _lastPosition = _rb.position;
    } 

    void ResetAccumulatedInputs()
    {
        _accumulatedInput = UnityEngine.Vector2.zero;
    }

    UnityEngine.Vector2 GetDirection(UnityEngine.Vector2 vectorA, UnityEngine.Vector2 vectorB)
    {
        return (vectorB - vectorA).normalized;
    }

}
