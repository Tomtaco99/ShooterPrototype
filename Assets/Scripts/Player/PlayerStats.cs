using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public static PlayerStats PlayerStatsInstance { get; private set; }

    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _currentHealth;
    public float CurrentHealth { get => _currentHealth;
        private set => _currentHealth = value;
    }
    public float MaxHealth { get => _maxHealth;
        private set => _maxHealth = value;
    }

    public event IDamagable.TakeDamageEvent OnTakeDamage;
    public event IDamagable.DeathEvent OnDeath;

    private void Awake()
    {
        OnTakeDamage += GetDamaged;
        OnDeath += Die;
        OnHeal += GetHealed;
        CurrentHealth = _maxHealth;
        if(PlayerStatsInstance == null)
            PlayerStatsInstance = this;
        else
            Destroy(gameObject);
    }
    
    public void Heal(float damage)
    {
        float hpToHeal = Mathf.Clamp(damage, 0, _maxHealth - _currentHealth);
        OnHeal?.Invoke(hpToHeal);
    }

    public event IDamagable.HealEvent OnHeal;

    public void TakeDamage(float damage = 1, Vector3 impactPos = default, Vector3 dir = default)
    {
        float damageToTake = Mathf.Clamp(damage, 0, _currentHealth);
        if (damageToTake != 0)
        {
            OnTakeDamage?.Invoke(damageToTake);
        }
        if (_currentHealth == 0)
        {
            OnDeath?.Invoke(transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollectibleBase>())
        {
            other.GetComponent<CollectibleBase>().GetCollected();
        }
    }

    private void GetDamaged(float damage)
    {
        CurrentHealth -= damage;
    }

    private void GetHealed(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + damage, 0, _maxHealth);
    }

    private void Die(Vector3 pos)
    {
        SceneManager.LoadScene("MainMenu");
    }

    public int CoinCount { get; set; }
}
