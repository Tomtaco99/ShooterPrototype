using UnityEngine;

public interface IDamagable
{
    public float CurrentHealth { get; }
    public float MaxHealth { get; }

    public delegate void TakeDamageEvent(float damage);
    public event TakeDamageEvent OnTakeDamage;

    public delegate void DeathEvent(Vector3 pos);
    public event DeathEvent OnDeath;

    public delegate void HealEvent(float damage);
    public event HealEvent OnHeal;
    
    public void TakeDamage(float damage = 1f, Vector3 impactPos = default, Vector3 dir = default);
}
