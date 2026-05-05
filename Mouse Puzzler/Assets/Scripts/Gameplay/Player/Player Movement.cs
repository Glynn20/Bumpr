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
    [SerializeField] private float _mouseSens; 
    [SerializeField] private PlayerMovementConfig _config;
    private float _minMouseSens = 0.01f;
    private float _maxMouseSens = 1.0f;
    private Rigidbody2D _rb;
    private UnityEngine.Vector2 _accumulatedInput;
    private bool _isInputEnabled = true;

    private float _currentSpeed; 
    public float GetCurrentSpeed() => _currentSpeed;
    private SpriteRenderer _sprite;

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
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();  
        _sprite = GetComponentInChildren<SpriteRenderer>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _mouseSens = Mathf.Clamp(_mouseSens, _minMouseSens, _maxMouseSens);
    }    
    void FixedUpdate()
    {
        if (_isInputEnabled == false) return;
        AdjustPosition();
    }
    void AdjustPosition()
    {
        if (_accumulatedInput != UnityEngine.Vector2.zero)
        {

            UnityEngine.Vector2 moveAmount = _accumulatedInput * _mouseSens * Time.fixedDeltaTime;
            UnityEngine.Vector2 targetPosition = _rb.position + moveAmount;
           
            _currentSpeed = moveAmount.magnitude / Time.fixedDeltaTime;

            _rb.MovePosition(targetPosition);

            UnityEngine.Vector2 direction = GetDirection(transform.position, targetPosition);
            if (direction.x < 0) _sprite.flipX = true;
            else                 _sprite.flipX = false;

            ResetAccumulatedInputs();
        }
        else
        {
            _rb.linearVelocity = UnityEngine.Vector2.zero;
            _currentSpeed = 0;
        }
    } 

    void ResetAccumulatedInputs()
    {
        _accumulatedInput = UnityEngine.Vector2.zero;
    }

    UnityEngine.Vector2 GetDirection(UnityEngine.Vector2 vectorA, UnityEngine.Vector2 vectorB)
    {
        return (vectorB - vectorA).normalized;
    }

    // public

}
