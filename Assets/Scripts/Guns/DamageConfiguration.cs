using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "Damage configuration", menuName = "Guns/Damage configuration", order = 5)]
public class DamageConfiguration : ScriptableObject
{
    public MinMaxCurve damageCurve;

    private void Reset()
    {
        damageCurve.mode = ParticleSystemCurveMode.Curve;
    }

    public int GetDamage(float distance = 0)
    {
        return Mathf.CeilToInt(damageCurve.Evaluate(distance, Random.value));
    }
}
