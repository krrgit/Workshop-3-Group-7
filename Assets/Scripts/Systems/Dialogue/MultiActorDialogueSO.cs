using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MultiActorDialogue", menuName = "ScriptableObjects/MultiActorDialogue", order = 1)]
public class MultiActorDialogueSO : ScriptableObject {
    public Color[] bubbleColors = {new Color(1,0.9f, 0.65f)};
    public Color[] textColors = {new Color(0.2078f,0.1098f, 0.1098f)};
    public string[] lines;

    /**Dialogue Parameters
     * Start of Line:
     * 0-n Actor; 0 = player, 1 = this actor, 2+ = others
     * 0-9 Speed: 5 = normal +- 0.5x;
     *
     * In-Line Events:
     * '>' Interrupt with next line
     * '[#' Hold # = 0-9
     */

    public Color GetBubbleColor(int currLine)
    {
        return bubbleColors[(int)System.Char.GetNumericValue(lines[currLine][0])];
    }
    
    public Color GetTextColor(int currLine)
    {
        return textColors[(int)System.Char.GetNumericValue(lines[currLine][0])];
    }

    public static float GetHoldDuration(int delay)
    {
        return delay * 0.1f;
    }

    public float GetSpeedModifier(int currLine)
    {
        return 1 + ((float)System.Char.GetNumericValue(lines[currLine][1]) - 5) * 0.167f;
    }
    
}
