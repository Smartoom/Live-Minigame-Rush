using UnityEngine;
using TMPro;

public class CrackTheEgg : Minigame
{
    [SerializeField] private SpriteRenderer Egg;
    [SerializeField] private Color[] EggCracks;
    [SerializeField] private TMP_Text EggCounter;

    protected int crackTimes = 0;

    private void OnEnable()
    {
        Debug.Log("Crack the egg minigame. Crack the egg before time runs out!");
    }

    public void UpdateEgg()
    {
        if (!isGameActive) { return; }
        crackTimes += 1;
        
        if(crackTimes >= 10)
        {
            EndMinigame(true);
        }else{
            EggCounter.text = $"{10 - crackTimes}";
            Egg.color = EggCracks[crackTimes];

            Debug.Log(crackTimes);
        }
    }
}