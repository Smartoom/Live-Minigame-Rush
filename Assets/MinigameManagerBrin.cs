using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinigameManagerBrin : MonoBehaviour
{
    public List<GameObject> minigamePrefabs;
    public static MinigameManagerBrin Instance;
    private GameObject currentMinigame;
/*    [SerializeField] private int chooseMinigameDebug = 0;*/
    void Awake()
    {
        if(Instance == null) { Instance = this; } else { Destroy(this); }
        StartNextMinigame(); // Start random minigame
    }

    public void StartNextMinigame()
    {
        if (currentMinigame != null)
        {
            Destroy(currentMinigame);
        }

        int index = Random.Range(0, minigamePrefabs.Count); //chooseMinigameDebug;
        currentMinigame = Instantiate(minigamePrefabs[index]);
        currentMinigame.GetComponent<Minigame>().StartMinigame();
    }

    public void OnMinigameEnd(bool success)
    {
        string result = success ? "won" : "lost";
        Debug.Log($"Player has {result} the minigame!");
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