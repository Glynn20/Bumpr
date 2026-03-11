using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
        // Must specifically be the Player
        IHealth health = other.gameObject.GetComponentInChildren<IHealth>();
        if (other.gameObject.GetComponentInChildren<IPlayer>() == null) return;
        
        health.TakeDamage();
    }
}
