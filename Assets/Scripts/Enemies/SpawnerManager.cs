using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform SpawnerParent;
    private Spawner[] _spawners;
    [SerializeField] private float timer;
    private float Cooldown;
    void Start()
    {
        _spawners = SpawnerParent.GetComponentsInChildren<Spawner>();
        Cooldown = Time.time + timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Cooldown < Time.time)) return;
        foreach (Spawner Sp in _spawners)
        {
            Random.Range(0, 2);
            if (Random.Range(0, 2) == 1)
            {
                Sp.Spawn();
            }
        }
        Cooldown = Time.time + timer + Random.Range(-3f, 3f);
    }
}
