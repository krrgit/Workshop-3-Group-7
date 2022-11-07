using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum BubbleAlign {
    Left,
    Center,
    Right
}

public class TextBubbleAnimator : MonoBehaviour {
    [SerializeField] private RectTransform rect;
    [Header("Text")]
    [SerializeField] private string dialogue;
    [SerializeField] private float letterPerSec = 3;
    [SerializeField] private float textDelay = 0.05f;
    
    [Header("Bubble")]
    [SerializeField] private Vector2 padding = new Vector2(10,15);
    [SerializeField] private AnimationCurve enterCurve;
    [SerializeField] private float bubbleAnimDur;
    [SerializeField] private BubbleAlign align = BubbleAlign.Center;
    [SerializeField] private float camPadding = 1f;
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI hiddenText;
    [SerializeField] private Image bubble;
    [SerializeField] private RectTransform tail;
    [SerializeField] private Transform tailTarget;

    private string formattedText = "";
    private string tempText = "";
    private int lineCount;
    private int lineWidth;

    private float defaultSize;
    private float defBubbleSize;
    float defTextSize;

    private bool isActive;
    
    // Camera Values
    private Vector2 screenBounds;

    private void OnDisable()
    {
        rect.localScale = Vector2.zero;
    }

    private void Start()
    {
        defaultSize = rect.lossyScale.x;
        defBubbleSize = bubble.rectTransform.lossyScale.x / defaultSize;
        defTextSize = textMesh.rectTransform.lossyScale.x / defaultSize;
        
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
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

    public void Animate(string newText, Transform target)
    {
        tailTarget = target;

        transform.position = tailTarget.position;
        gameObject.SetActive(true);
        dialogue = newText;
        if (!isActive)
        {
            AnimateOpen();
            isActive = true;
        }
        else
        {
            AnimateRefresh();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        isActive = false;
    }

    void AnimateOpen()
    {
        StopAllCoroutines();
        FormatString();
        StartCoroutine(AnimateText(0));
        StartCoroutine(AnimateWholeBubble());
    }

    void AnimateRefresh()
    {
        StopAllCoroutines();
        FormatString();
        StartCoroutine(AnimateText(bubbleAnimDur/2f));
        StartCoroutine(AnimateTop());
    }
    
    // Open Text Bubble
    IEnumerator AnimateWholeBubble()
    {
        float bubbleTimer = 0;
        yield return new WaitForEndOfFrame();
        ResizeBubble();

        while (bubbleTimer < 1)
        {
            rect.localScale = Vector2.one * enterCurve.Evaluate(bubbleTimer) * defaultSize;
            bubbleTimer += Time.deltaTime/ bubbleAnimDur;
            yield return new WaitForEndOfFrame();
        }

        rect.localScale = Vector2.one * defaultSize;
    }
    
    // Used for the update text animation
    IEnumerator AnimateTop()
    {
        float bubbleTimer = 0;
        // Close
        while (bubbleTimer < 1)
        {
            bubble.rectTransform.localScale = Vector2.one * enterCurve.Evaluate(1f-bubbleTimer) * defBubbleSize;
            bubbleTimer += Time.deltaTime * 2f / bubbleAnimDur;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForEndOfFrame();
        ResizeBubble();
        
        bubbleTimer = 0;
        // Open
        while (bubbleTimer < 1)
        {
            bubble.rectTransform.localScale = Vector2.one * enterCurve.Evaluate(bubbleTimer) * defBubbleSize;
            bubbleTimer += Time.deltaTime / bubbleAnimDur;
            yield return new WaitForEndOfFrame();
        }

        bubble.rectTransform.localScale = Vector2.one * defBubbleSize;
    }
    
    IEnumerator AnimateText(float wait)
    {
        int i = 0;
        yield return new WaitForSeconds(wait);
        yield return new WaitForEndOfFrame();
        FormatTextMesh();
        yield return new WaitForSeconds(textDelay);
        

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
        var textPos = textMesh.rectTransform.localPosition;
        textPos.y = padding.y * 0.5f;
        textMesh.rectTransform.localPosition = textPos; 
        textMesh.rectTransform.sizeDelta = hiddenText.GetRenderedValues();
    }

    void ResizeBubble()
    {
        bubble.rectTransform.sizeDelta = (hiddenText.GetRenderedValues() * defTextSize) + padding;
        PlaceBubble();
    }
    
    // Places bubble on top of target
    void PlaceBubble()
    {
        if (!tail) return;
        transform.position = new Vector3(tailTarget.position.x,tailTarget.position.y, transform.position.z);
        MoveBubblePivot();
        ClampBubbleToView();
        ClampBubbleToTail();
    }
    
    // This moves where the bubble should be relative to target
    void MoveBubblePivot()
    {
        var bubblePos = bubble.rectTransform.localPosition;
        var pivot = bubble.rectTransform.pivot;
        switch (align)
        {
            case BubbleAlign.Center:
                pivot = Vector2.right * 0.5f;
                bubblePos.x = 0;
                break;
            case BubbleAlign.Left:
                pivot = Vector2.zero;
                bubblePos.x = -padding.x;
                break;
            case BubbleAlign.Right:
                pivot = Vector2.right;
                bubblePos.x = padding.x;
                break;
            default:
                break;
        }
        bubble.rectTransform.localPosition = bubblePos;
        bubble.rectTransform.pivot = pivot;
    }

    void ClampBubbleToView()
    {
        var bubblePos = bubble.rectTransform.localPosition + rect.localPosition;
        var deltaX = screenBounds.x - (bubble.rectTransform.sizeDelta.x * (align==BubbleAlign.Center ?  0.5f: 1)) - camPadding;
        
        bubblePos.x = Mathf.Clamp(bubblePos.x,-deltaX,deltaX);
        print("bounds: " + deltaX + " | pos: " + bubblePos.x);

        bubblePos -= rect.localPosition;
        bubble.rectTransform.localPosition = bubblePos;
    }
    
    void ClampBubbleToTail()
    {
        var bubblePos = bubble.rectTransform.localPosition;
        var deltaX = (bubble.rectTransform.sizeDelta.x / 2f) - padding.x;
        print("width: "  + bubble.rectTransform.sizeDelta.x);

        switch (align)
        {
            case BubbleAlign.Center:
                bubblePos.x = Mathf.Clamp(bubblePos.x,-deltaX,deltaX);
                break;
            case BubbleAlign.Left:
                bubblePos.x = Mathf.Clamp(bubblePos.x, -(bubble.rectTransform.sizeDelta.x - padding.x),-padding.x);
                break;
            case BubbleAlign.Right:
                bubblePos.x = Mathf.Clamp(bubblePos.x,padding.x, (bubble.rectTransform.sizeDelta.x - padding.x));
                break;
        }
        bubble.rectTransform.localPosition = bubblePos;
    }

}
