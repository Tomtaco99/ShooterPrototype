using UnityEngine;

public class Cube : MonoBehaviour, IDamagable
{
    private Rigidbody _rb;
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
    
    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        OnTakeDamage += GetDamaged;
        OnTakeDamage += IncreasePlayerScore;
        OnDeath += Die;
        OnDeath += IncreasePlayerScore;
    }

    private void IncreasePlayerScore(Vector3 pos)
    {
    }

    private void IncreasePlayerScore(float damage)
    {
    }


    private void GetDamaged(float damage)
    {
        CurrentHealth -= damage;
    }

    private void Die(Vector3 pos)
    {
        Destroy(gameObject);
    }

    public event IDamagable.HealEvent OnHeal;

    public void TakeDamage(float damage = 1f, Vector3 impactPos = default, Vector3 dir = default)
    {
        _rb.AddForceAtPosition(dir * Random.Range(5, 10), impactPos, ForceMode.Impulse);
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
    
}
