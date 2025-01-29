using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class OrderTheNumbers : Minigame
{
    [SerializeField] private TMP_Text NumberDisplay;
    private List<int> orderedNumbers, currentNumbers;

    private void OnEnable()
    {
        currentNumbers = new List<int>();
        orderedNumbers = new List<int>();

        for (int i = 0; i < 9; i++) { currentNumbers.Add(UnityEngine.Random.Range(0, 9)); }
        orderedNumbers = new List<int>(currentNumbers);
        orderedNumbers.Sort();
        GetNumbersAsString();
    }

    private void Update()
    {
        if (!isGameActive) { return; }

        for (int key = 0; key < 9; key++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + key) && orderedNumbers[0] == key)
            {
                if (orderedNumbers.Contains(key))
                {
                    orderedNumbers.Remove(key);
                    currentNumbers.Remove(key);
                    GetNumbersAsString();
                }
                else
                {
                    Debug.Log("Invalid key: " + key + " check list");
                }

                break;
            }
        }

        if(orderedNumbers.Count == 0)
        {
            EndMinigame(true);
        }
    }

    private void GetNumbersAsString()
    {
        NumberDisplay.text = string.Join(" - ", currentNumbers);
        return;
    }
}
