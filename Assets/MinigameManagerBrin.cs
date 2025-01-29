using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinigameManagerBrin : MonoBehaviour
{
    public List<GameObject> minigamePrefabs;
    private int lastGamePlayedInt = -1;
    public static MinigameManagerBrin Instance;
    private GameObject currentMinigame;

    [Header("Health")]
    [SerializeField] private int startHealth = 3;
    private int health;
    [SerializeField] private float uiHeartInterval = 80; //used for ui heart spacing
    private bool gameOver = false;

    private int points;

    void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); }
    }
    private void Start()
    {
        health = startHealth;
        ReMakeHealthHearts();
        StartNextMinigame(); // Start random minigame
    }
    private void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }
    public void DestroyCurrentGame()
    {
        if (currentMinigame != null)
        {
            Destroy(currentMinigame);
        }
    }
    public void StartNextMinigame()
    {
        int newRandomGameInt = Random.Range(0, minigamePrefabs.Count);
        while (lastGamePlayedInt == newRandomGameInt && minigamePrefabs.Count >= 2)
        {
            newRandomGameInt = Random.Range(0, minigamePrefabs.Count);
        }
        currentMinigame = Instantiate(minigamePrefabs[newRandomGameInt]);
        currentMinigame.GetComponent<Minigame>().StartMinigame();
        lastGamePlayedInt = newRandomGameInt;
    }

    public void GainPoint()
    {
        points += 1;
        UIReferenceManager.instance.gameScreenscoreText.text = points.ToString();
    }
    public void LoseLife()
    {
        health -= 1;
        ReMakeHealthHearts();
    }
    private void ReMakeHealthHearts()
    {
        for (int i = 0; i < UIReferenceManager.instance.heartsParent.childCount; i++)
        {
            Destroy(UIReferenceManager.instance.heartsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < health; i++)
        {
            Instantiate(UIReferenceManager.instance.heartPrefab, UIReferenceManager.instance.heartsParent.position, Quaternion.identity, UIReferenceManager.instance.heartsParent).transform.localPosition = new Vector3(uiHeartInterval * i, 0, 0);
        }
    }
    private void RestartGame()
    {
        gameOver = false;
        health = startHealth;
        ReMakeHealthHearts();
        points = 0;
        UIReferenceManager.instance.gameScreenscoreText.text = points.ToString();
        StartNextMinigame();
        UIReferenceManager.instance.GameScreen.SetActive(true);
        UIReferenceManager.instance.GameOverScreen.SetActive(false);
    }

    public void OnMinigameEnd(bool success)
    {
        DestroyCurrentGame();
        if (success)
        {
            GainPoint();
            StartNextMinigame();
        }
        else
        {
            LoseLife();
            if (health <= 0)
            {
                gameOver = true;
                UIReferenceManager.instance.GameScreen.SetActive(false);
                UIReferenceManager.instance.GameOverScreen.SetActive(true);
                UIReferenceManager.instance.gameOverscoreText.text = points.ToString();
                DestroyCurrentGame();
            }
            else
            {
                StartNextMinigame();
            }
        }
    }
}

public abstract class Minigame : MonoBehaviour
{
    public float timeLimit = 5f;
    protected bool isGameActive;

    public virtual void StartMinigame()
    {
        isGameActive = true;
        StartCoroutine(MinigameTimer());
    }

    public virtual void EndMinigame(bool success)
    {
        isGameActive = false;
        MinigameManagerBrin.Instance.OnMinigameEnd(success);
        this.gameObject.SetActive(false);
    }

    private IEnumerator MinigameTimer()
    {
        yield return new WaitForSeconds(timeLimit);
        if (isGameActive)
        {
            EndMinigame(false);
        }
    }
}