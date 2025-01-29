using UnityEngine;

public class PopTheBalloon : Minigame
{
    [SerializeField] private GameObject balloonPrefab, balloonContainer;
    [SerializeField] private Vector2 spawnAreaMin, spawnAreaMax;
    private int baloonsSpawned = 80085;

    private void OnEnable()
    {
        Debug.Log("Pop The Balloon minigame, left click button to pop a balloon before time runs out");

        SpawnRandomBalloons();
    }

    private void SpawnRandomBalloons()
    {
        baloonsSpawned = Random.Range(3, 5); // N Balloons => between 3 and 5

        for (int i = 0; i < baloonsSpawned; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f);

            Instantiate(balloonPrefab, randomPos, Quaternion.identity, balloonContainer.transform);
        }
    }
    private void Update()
    {
        foreach (Transform child in balloonContainer.transform)
        {
            if (child.gameObject.activeSelf)
            {
                return;
            }
        }
        EndMinigame(true);
    }
}