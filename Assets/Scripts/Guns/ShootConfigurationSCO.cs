using UnityEngine;

[CreateAssetMenu(fileName = "Shoot configuration", menuName = "Guns/Shoot configuration", order = 2)]
public class ShootConfigurationSCO : ScriptableObject
{
    //This scriptable determines how the weapon shoots and what it hits.
    public LayerMask hitMask;
    public Vector3 Recoil = new Vector3(0f, 0f, 0f); //Shotguns and so on
    public float DistanceDeviation;
    public float SnapStrength;
    public float recoilRecoverySpeed;
    public float fireRate = 0.25f;
    public int projectilesPerShot = 1;
    public bool UsesProyectiles = false;
    public GameObject ProyectileToShoot;
    public Vector3 GetSpread()
    {
        return new Vector3(Recoil.x, Random.Range(-Recoil.y, Recoil.y), Random.Range(-Recoil.z, Recoil.z));
    }

    public Vector3 BulletDeviation()
    {
        return new Vector3(Random.Range(DistanceDeviation, -DistanceDeviation), Random.Range(DistanceDeviation, -DistanceDeviation), Random.Range(DistanceDeviation, -DistanceDeviation));
    }
}
