using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dialogue Player: Responsible for parsing lines and displaying them as they are notated.
public class DialoguePlayer : MonoBehaviour {
    [SerializeField] private MultiActorDialogueSO so;
    [SerializeField] private TextBubbleAnimator[] bubbles;
    [SerializeField] private Transform[] actors = new Transform[2];

    private int currLine = 0;
    private int count;
    private int lastActor;
    
    public void ShowText()
    {
        // Actor 0 = Player; 1 = This Actor; 2+ others
        int actorIndex = (int)System.Char.GetNumericValue(so.lines[currLine][0]);
        
        // Hide last bubble when speaking actor changes
        if (currLine != 0)
        {
            if (lastActor != actorIndex)
            {
                bubbles[lastActor].Hide();
            }
        }
        
        SetBubbleColors(actorIndex, so.GetBubbleColor(currLine), so.GetTextColor(currLine));
        
        bubbles[actorIndex].SetSpeed(so.GetSpeedModifier(currLine));

        // Animate Text Bubble
        bubbles[actorIndex].Animate(so.lines[currLine], GetActor(actorIndex));
        ++currLine;
        lastActor = actorIndex;
        //SoundManager.Instance.Playhehe();
    }
    

    public void HideText()
    {
        // Actor 0 = Player; 1 = Actor
        bubbles[lastActor].Hide();
        currLine = 0;
    }

    void SetBubbleColors(int actor, Color bubbleColor, Color textColor)
    {
        bubbles[actor].SetColors(bubbleColor,textColor);
    }

    Transform GetActor(int actorIndex)
    {
        return actors[actorIndex];
    }
    
}
