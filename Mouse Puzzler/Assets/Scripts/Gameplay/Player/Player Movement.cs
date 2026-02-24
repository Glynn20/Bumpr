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

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _mouseSens = Mathf.Clamp(_mouseSens, _minMouseSens, _maxMouseSens);
    }    
    void AdjustPosition()
    {
        if (_accumulatedInput != UnityEngine.Vector2.zero)
        {

            UnityEngine.Vector2 moveAmount = _accumulatedInput * _mouseSens * Time.fixedDeltaTime;
            UnityEngine.Vector2 targetPosition = _rb.position + moveAmount;

            _currentSpeed = moveAmount.magnitude / Time.fixedDeltaTime;

            _rb.MovePosition(targetPosition);

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

    void FixedUpdate()
    {
        if (_isInputEnabled == false) return;
        AdjustPosition();
    }

    // public
    public void ReadMouseInput(InputAction.CallbackContext context)
    {
        if (_isInputEnabled == false) return;
        _accumulatedInput += context.ReadValue<UnityEngine.Vector2>();
    }

    public void DisableInput()
    {
        _isInputEnabled = false;
        ResetAccumulatedInputs();
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
}
