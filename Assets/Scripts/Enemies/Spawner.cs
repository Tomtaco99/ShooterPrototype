using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyPrefabs;
    // Start is called before the first frame update
    public void Spawn()
    {
        Instantiate(EnemyPrefabs[Random.Range(0, 2)], transform.position, quaternion.identity);
    }
}
