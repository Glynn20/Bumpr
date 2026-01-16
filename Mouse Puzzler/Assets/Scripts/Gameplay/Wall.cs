using System.Security;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Must specifically be the Player
        IKillable killable = other.gameObject.GetComponentInChildren<IKillable>();
        IMovement movement = other.gameObject.GetComponent<IMovement>();

        if ( movement == null || killable == null ) return;
        if ( movement.GetMoveState() != PlayerMoveState.Overheating ) return;
        
        killable.Die();
    }
}
