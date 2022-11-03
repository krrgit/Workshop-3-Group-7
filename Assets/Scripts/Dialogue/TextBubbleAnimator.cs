using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBubbleAnimator : MonoBehaviour {
    [SerializeField] private RectTransform rect;
    [Header("Text")]
    [SerializeField] private string dialogue;
    [SerializeField] private float letterPerSec = 3;
    [SerializeField] private float lineHeight = 14;
    [SerializeField] private float delay = 0.05f;
    
    [Header("Bubble")]
    [SerializeField] private float charWidth = 20;
    [SerializeField] private float charHeight = 26;
    [SerializeField] private float lineSpacing = 0.8f;
    [SerializeField] private Vector2 padding = new Vector2(10,15);
    [SerializeField] private AnimationCurve enterCurve;
    [SerializeField] private float bubbleAnimDur;
    
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI hiddenText;
    
    [SerializeField] private Image bubble;


    private string formattedText = "";
    private string tempText = "";
    private int lineCount;
    private int lineWidth;

    private float defaultSize;
    private float defBubbleSize;
    float defTextSize;

    private void Start()
    {
        defaultSize = rect.lossyScale.x;
        defBubbleSize = bubble.rectTransform.lossyScale.x / defaultSize;
        defTextSize = textMesh.rectTransform.lossyScale.x / defaultSize;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AnimateOpen();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AnimateRefresh();
        }
    }

    void AnimateOpen()
    {
        StopAllCoroutines();
        FormatString();
        StartCoroutine(AnimateText());
        StartCoroutine(AnimateWholeBubble());
    }

    void AnimateRefresh()
    {
        StopAllCoroutines();
        FormatString();
        StartCoroutine(AnimateText());
        StartCoroutine(AnimateTop());
    }
    
    IEnumerator AnimateWholeBubble()
    {
        float bubbleTimer = 0;
        yield return new WaitForEndOfFrame();
        ResizeBubble();

        while (bubbleTimer < 1)
        {
            bubbleTimer += Time.deltaTime / bubbleAnimDur;
            rect.localScale = Vector2.one * enterCurve.Evaluate(bubbleTimer) * defaultSize;
            yield return new WaitForEndOfFrame();
        }

        rect.localScale = Vector2.one * defaultSize;
    }

    IEnumerator AnimateTop()
    {
        float bubbleTimer = 0;
        yield return new WaitForEndOfFrame();
        ResizeBubble();

        while (bubbleTimer < 1)
        {
            bubbleTimer += Time.deltaTime / bubbleAnimDur;
            bubble.rectTransform.localScale = Vector2.one * enterCurve.Evaluate(bubbleTimer) * defBubbleSize;
            textMesh.rectTransform.localScale = Vector2.one * enterCurve.Evaluate(bubbleTimer) * defTextSize;
            yield return new WaitForEndOfFrame();
        }

        bubble.rectTransform.localScale = Vector2.one * defBubbleSize;
        textMesh.rectTransform.localScale = Vector2.one * defTextSize;
    }
    
    IEnumerator AnimateText()
    {
        int i = 0;
        yield return new WaitForEndOfFrame();
        FormatTextMesh();
        yield return new WaitForSeconds(delay);
        

        while (i < dialogue.Length)
        {
            // Animate Text
            tempText = tempText + formattedText[i];
            textMesh.text = tempText;
            
            yield return new WaitForSeconds(1f / letterPerSec);
            ++i;
        }
    }

    void FormatString()
    {
        textMesh.text = " ";
        tempText = "";

        formattedText = dialogue;
        formattedText = formattedText.Replace('|','\n');
        lineCount = dialogue.Split('|').Length;
        hiddenText.text = formattedText;

        if (formattedText.Contains('\n'))
        {
            int firstLineLength = formattedText.Split('\n')[0].Length;
            int secondLineLength = formattedText.Split('\n')[1].Length;
            lineWidth = firstLineLength > secondLineLength ? firstLineLength : secondLineLength;   
        }
    }

    void FormatTextMesh()
    {
        textMesh.rectTransform.localPosition = Vector3.up * padding.y * 0.5f; 
        textMesh.rectTransform.sizeDelta = hiddenText.GetRenderedValues();
    }

    void ResizeBubble()
    {
        bubble.rectTransform.sizeDelta = (hiddenText.GetRenderedValues() * 0.1f) + padding;
    }
    
    
    
}
