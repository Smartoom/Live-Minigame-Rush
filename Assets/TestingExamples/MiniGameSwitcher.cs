using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSwitcher : MonoBehaviour
{
    public static MiniGameSwitcher instnace;

    [SerializeField] private GameObject[] miniGameprefabs;
    private GameObject currentLoadedMiniGame;

    private void Awake()
    {
        if (instnace != null)
        {
            Destroy(gameObject);
            return;
        }
        instnace = this;
    }
    private void Start()
    {
        NextGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            NextGame();
        }
    }
    [ContextMenu("Next Mini Game")]
    public void NextGame()
    {
        Destroy(currentLoadedMiniGame);
        currentLoadedMiniGame = Instantiate(miniGameprefabs[Random.Range(0, miniGameprefabs.Length)]);//should check if the next game is the one we just beat
    }
}
