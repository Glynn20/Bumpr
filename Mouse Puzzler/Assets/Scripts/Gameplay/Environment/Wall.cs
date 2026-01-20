using System.Security;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        // Must specifically be the Player
        IHealth killable = other.gameObject.GetComponentInChildren<IHealth>();
        IMovement movement = other.gameObject.GetComponent<IMovement>();
        if ( movement == null || killable == null ) return;

        if ( movement.GetMoveState() != PlayerMoveState.Overheating ) return;
        
        killable.Die();
    }

}
