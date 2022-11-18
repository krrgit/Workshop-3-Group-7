using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDialogue : MonoBehaviour {
    [SerializeField] private TextBubbleAnimator bubble;
    [SerializeField] private string[] text;

    private int count;

    public void ShowText()
    {
        bubble.Animate(text[count],transform);
        ++count;
    }

    public void HideText()
    {
        bubble.Hide();
        count = 0;
    }
}
