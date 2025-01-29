using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    [Tooltip("minimum of two games needed")]
    [SerializeField] private GameObject[] miniGameprefabs;
    private GameObject currentLoadedMiniGame; //used to destroy the game after the game is done.
    private int lastGamePlayedInt = -1;//idk if this should be readonly or smtth

    [SerializeField] private KeyCode editorGameSkipKey;

    [Header("Health")]
    [SerializeField] private int startHealth = 3;
    private int health;
    [SerializeField] private float uiHeartInterval = 80; //used for ui heart spacing
    private bool gameOver = false;

    private int points;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        health = startHealth;
        ReMakeHealthHearts();
        NextGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(editorGameSkipKey) && Application.isEditor)
        {
            NextGame();
        }
        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }
    /// <summary>
    /// Destroy the current game and start a new random one.
    /// </summary>
    [ContextMenu("Next Mini Game")]
    public void NextGame()
    {
        if (gameOver)
            return;
        Destroy(currentLoadedMiniGame);

        int newRandomGameInt = Random.Range(0, miniGameprefabs.Length);
        while (lastGamePlayedInt == newRandomGameInt && miniGameprefabs.Length >= 2)
        {
            newRandomGameInt = Random.Range(0, miniGameprefabs.Length);
        }
        currentLoadedMiniGame = Instantiate(miniGameprefabs[newRandomGameInt]);
        lastGamePlayedInt = newRandomGameInt;
    }
    /// <summary>
    /// Stops the game
    /// </summary>
    private void RemoveGame()
    {
        Destroy(currentLoadedMiniGame);
    }
    /// <summary>
    /// Gain a point
    /// </summary>
    [ContextMenu("Goin Point")]
    public void GainPoint()
    {
        points += 1;
        UIReferenceManager.instance.gameScreenscoreText.text = points.ToString();
    }
    /// <summary>
    /// lose a heart. if none are left then the game is over and the active game is destroyed.
    /// </summary>
    [ContextMenu("Lose Life")]
    public void LoseLife()
    {
        health -= 1;
        ReMakeHealthHearts();
        if (health <= 0)
        {
            gameOver = true;
            UIReferenceManager.instance.GameScreen.SetActive(false);
            UIReferenceManager.instance.GameOverScreen.SetActive(true);
            UIReferenceManager.instance.gameOverscoreText.text = points.ToString();
            RemoveGame();
        }
    }
    /// <summary>
    /// Update the health hearts in the UI
    /// </summary>
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
        NextGame();
        UIReferenceManager.instance.GameScreen.SetActive(true);
        UIReferenceManager.instance.GameOverScreen.SetActive(false);
    }
}
