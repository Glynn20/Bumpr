using System.Security;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Must specifically be the Player
        IHealth killable = other.gameObject.GetComponentInChildren<IHealth>();
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;
        
        killable.Die();
    }
}
