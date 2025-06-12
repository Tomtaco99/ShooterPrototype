using UnityEngine;

[RequireComponent(typeof(IDamagable))]
public class SpawnParticleOnDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    public IDamagable damagable;

    private void Awake()
    {
        damagable = GetComponent<IDamagable>();
    }

    private void OnEnable()
    {
        damagable.OnDeath += OnDeathEffect;
    }

    private void OnDeathEffect(Vector3 pos)
    {
        Instantiate(deathEffect, pos, Quaternion.identity);
    }
}
