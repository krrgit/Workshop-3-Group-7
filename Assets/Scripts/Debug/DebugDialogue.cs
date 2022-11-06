using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDialogue : MonoBehaviour {
    [SerializeField] private TextBubbleAnimator bubble;
    [SerializeField] private string text;

    public void ShowText()
    {
        bubble.Animate(text,transform);
    }

    public void HideText()
    {
        bubble.Hide();
    }
}
