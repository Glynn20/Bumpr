using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Must specifically be the Player
        IHealth health = other.gameObject.GetComponentInChildren<IHealth>();
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;
        
        health.TakeDamage();
    }
}
