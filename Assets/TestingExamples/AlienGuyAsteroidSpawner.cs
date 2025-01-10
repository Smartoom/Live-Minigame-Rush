using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGuyAsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    private float lastSpawned;
    [SerializeField] private GameObject asteroidPrefab;
    private void Update()
    {
        lastSpawned += Time.deltaTime;
        if (lastSpawned >= spawnInterval)
        {
            Instantiate(asteroidPrefab, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0), Quaternion.identity, transform.parent);
            lastSpawned = 0;
        }
    }
}
