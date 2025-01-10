using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReferenceManager : MonoBehaviour
{
    public static UIReferenceManager instance;

    public GameObject GameScreen;
    public GameObject GameOverScreen;

    public Transform heartsParent;
    public GameObject heartPrefab;
    public Text scoreText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
}
