using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _currentHealth;

    private EnemyBase enemy;
    [SerializeField] private GameObject[] ItemsToDrop;
    [SerializeField] private int[] Chances;

    public float CurrentHealth { get => _currentHealth;
        private set => _currentHealth = value;
    }
    public float MaxHealth { get => _maxHealth;
        private set => _maxHealth = value;
    }
    public event IDamagable.TakeDamageEvent OnTakeDamage;
    public event IDamagable.DeathEvent OnDeath;
    public event IDamagable.HealEvent OnHeal;
    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnTakeDamage += GetDamaged;
        enemy = transform.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            OnDeath += enemy.Death;
            OnDeath += dropItem;
        }
    }

    

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
    
    private void GetDamaged(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Ouch " + _currentHealth);
    }

    private void dropItem(Vector3 pos)
    {
        int i = Random.Range(0, ItemsToDrop.Length);
        if (Random.Range(0, 100) < Chances[i])
        {
            Instantiate(ItemsToDrop[i], pos + Vector3.up, quaternion.identity);
        }
    }
}
