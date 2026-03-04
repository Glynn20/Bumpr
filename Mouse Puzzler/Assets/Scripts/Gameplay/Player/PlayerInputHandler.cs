using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private GameEvent PausePressed;
    public void Pause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        PausePressed?.Raise(this, null);
    }

}
