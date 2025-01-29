using UnityEngine;
using TMPro;

public class MatchTheColor : Minigame
{
    [SerializeField] private SpriteRenderer[] Panels;
    [SerializeField] private SpriteRenderer correctColor;

    protected Color currentColor;
    protected int rightColors = 0;

    private void OnEnable()
    {
        Debug.Log("Match the color minigame. Click the right colored panel in");
        ChangeColors();
    }

    public void UpdateColor(SpriteRenderer panel)
    {
        if (!isGameActive) { return; }
        
        if(panel.color == currentColor)
        {
            Debug.Log("Player selected the correct color, moving to next");
            rightColors += 1;
        }else {
            Debug.Log("Wrong panel! Reloading");
        }

        if(rightColors >= 5) { EndMinigame(true); }
        ChangeColors();
    }

    private void ChangeColors()
    {
        foreach(SpriteRenderer panel in Panels)
        {
            panel.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        currentColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Panels[Random.Range(0, Panels.Length)].color = currentColor;
        correctColor.color = currentColor;
    }
}