using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Weapon", menuName = "Guns/Weapon", order = 0)]
public class WeaponSCO : ScriptableObject
{
    public string gunName;
    public GameObject weaponPrefab;
    public Vector3 spawnPoint;
    public Vector3 spawnRotation;
    public Vector3 AimPosition;
    private Vector3 CurrentRot;
    private Vector3 TargetRot;
    public float AimSpeed;
    public ShootConfigurationSCO shootConfiguration;
    public TrailConfigurationSCO trailConfiguration;
    public ReloadConfiguration reloadConfiguration;
    public DamageConfiguration damageConfiguration;

    private MonoBehaviour _activeBehavior;
    private GameObject _model;
    private float _lastShootTime;
    private float _onClickDown;
    private bool _attemptedShootLastFrame;
    private ParticleSystem _shootingParticle;
    private ObjectPool<TrailRenderer> _trailPool;
    private Transform _camera;
    private RecoilManager _recoil;
    private Transform _pivot;
    public GameObject Spawn(Transform parent, MonoBehaviour activeBehavior, Transform cam, RecoilManager r)
    {
        _activeBehavior = activeBehavior;
        _lastShootTime = 0;
        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        _camera = cam;
        _model = Instantiate(weaponPrefab, parent, false);
        _model.transform.localPosition = spawnPoint;
        _model.transform.localRotation = Quaternion.Euler(spawnRotation);
        _recoil = r;
        _pivot = parent;
        _shootingParticle = _model.GetComponentInChildren<ParticleSystem>();
        return _model;
    }

    public void Tick(bool attemptedShoot)
    {
        if (attemptedShoot)
        {
            _attemptedShootLastFrame = true;
            if (reloadConfiguration.currentMagAmmo > 0)
            {
                Shoot();
            }
        }
        else if (_attemptedShootLastFrame)
        {
            _attemptedShootLastFrame = false;
        }
    }

    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = trailConfiguration.color;
        trail.material = trailConfiguration.material;
        trail.widthCurve = trailConfiguration.widthCurve;
        trail.time = trailConfiguration.time;
        trail.minVertexDistance = trailConfiguration.minVertexDistance;
        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        return trail;
    }

    private void Shoot()
    {
        if (!(Time.time > shootConfiguration.fireRate + _lastShootTime)) return;
        _shootingParticle.Emit(1);
        _shootingParticle.Play();
        _shootingParticle.GetComponent<AudioSource>().Play();
        reloadConfiguration.currentMagAmmo--;
        _lastShootTime = Time.time;
        _recoil._targetRotation += shootConfiguration.GetSpread();
        for (int i = 0; i < shootConfiguration.projectilesPerShot; i++)
        {
            var shootDirection = (_model.transform.forward + shootConfiguration.BulletDeviation()).normalized;
            if (Physics.Raycast(_camera.transform.position, shootDirection, out RaycastHit hit, float.MaxValue,
                    shootConfiguration.hitMask))
            {
                Vector3 a = _shootingParticle.transform.position -
                            (hit.point - _shootingParticle.transform.position).normalized * 6f;
                Vector3 b = hit.point;
                Debug.DrawLine(a, b, Color.blue, 2f);
                if (!shootConfiguration.UsesProyectiles)
                {
                    Physics.Raycast(
                        _shootingParticle.transform.position -
                        (hit.point - _shootingParticle.transform.position).normalized, b - a, out RaycastHit hit2,
                        float.MaxValue, shootConfiguration.hitMask);
                    _activeBehavior.StartCoroutine(PlayTrail(_shootingParticle.transform.position, hit2.point, hit2));
                }
                else
                {
                    GameObject go = Instantiate(shootConfiguration.ProyectileToShoot, _shootingParticle.transform.position, Quaternion.identity);
                    go.transform.forward = b - a;
                }
            }
            else
            {
                if (!shootConfiguration.UsesProyectiles)
                {
                    _activeBehavior.StartCoroutine(PlayTrail(_shootingParticle.transform.position,
                        _camera.transform.position + (shootDirection * trailConfiguration.missDistance),
                        new RaycastHit()));
                }
                else
                {
                    GameObject go = Instantiate(shootConfiguration.ProyectileToShoot, _shootingParticle.transform.position, Quaternion.identity);
                    go.transform.forward = shootDirection;
                }
            }
        }
    }

    private IEnumerator PlayTrail(Vector3 start, Vector3 end, RaycastHit hit)
    {
        var instance = _trailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = start;
        yield return null;
        instance.emitting = true;
        var distance = Vector3.Distance(start, end);
        var remainingDistance = distance;
        while (remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(start, end, Mathf.Clamp01(1 - (remainingDistance / distance)));
            remainingDistance -= trailConfiguration.simulatedSpeed * Time.deltaTime;
            yield return null;
        }

        instance.transform.position = end;
        
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out IDamagable col))
            {
                col.TakeDamage(damageConfiguration.GetDamage(distance), hit.point, (end - start).normalized);
            }
            var a = Instantiate(trailConfiguration.bullethole, hit.point, Quaternion.LookRotation(hit.normal));
            if(a != null && hit.collider != null)
                a.transform.parent = hit.collider.transform;
        }

        yield return new WaitForSeconds(trailConfiguration.time);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        
        _trailPool.Release(instance);
    }

    public void Enable(bool state)
    {
        _model.SetActive(state);
    }

    
}
