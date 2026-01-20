using UnityEngine;

public interface IHealth
{
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    public void TakeDamage();
    public void Die();
}
