using System.Security;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Must specifically be the Player
        IKillable killable = other.gameObject.GetComponentInChildren<IKillable>();
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;
        
        killable.Die();
    }
}
