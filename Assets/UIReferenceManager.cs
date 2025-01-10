using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReferenceManager : MonoBehaviour
{
    public static UIReferenceManager instance;

    public GameObject GameScreen;
    public GameObject GameOverScreen;
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
