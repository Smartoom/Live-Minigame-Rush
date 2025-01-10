using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGuyAsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDistanceFromCenter;
    private float lastSpawned;
    [SerializeField] private GameObject asteroidPrefab;
    private void Update()
    {
        lastSpawned += Time.deltaTime;
        if (lastSpawned >= spawnInterval)
        {
            Instantiate(asteroidPrefab, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * spawnDistanceFromCenter, Quaternion.identity, transform.parent);
            lastSpawned = 0;
        }
    }
}
