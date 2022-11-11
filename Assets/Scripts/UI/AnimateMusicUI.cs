using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMusicUI : MonoBehaviour {
    [SerializeField] private RectTransform rect;
    [SerializeField] private float activeYPos = 0;
    [SerializeField] private float unactiveYPos = -367;
    [SerializeField] private float speed = 8;
    [SerializeField] private NotePlacer notePlacer;

    private bool isActive;
    private Vector2 pos;

    void Start()
    {
        pos.y = unactiveYPos;
        rect.anchoredPosition = pos;
        notePlacer.enabled = false;
    }

    public void Animate(bool isOpen)
    {
        StopAllCoroutines();
        if (isOpen)
        {
            StartCoroutine(Open());
        }
        else
        {
            StartCoroutine(Close());
        }
    }

    IEnumerator Open()
    {
        pos = rect.anchoredPosition;
        pos.y = unactiveYPos;

        while (pos.y < activeYPos - 0.1f)
        {
            pos.y = Mathf.Lerp(pos.y, activeYPos, Time.deltaTime * speed);
            rect.anchoredPosition = pos;
            yield return new WaitForEndOfFrame();
        }

        pos.y = activeYPos;
        rect.anchoredPosition = pos;
        notePlacer.enabled = true;
    }
    
    IEnumerator Close()
    {
        pos = rect.anchoredPosition;
        pos.y = activeYPos;
        notePlacer.enabled = false;

        while (pos.y > unactiveYPos + 0.1f)
        {
            pos.y = Mathf.Lerp(pos.y, unactiveYPos, Time.deltaTime * speed);
            rect.anchoredPosition = pos;
            yield return new WaitForEndOfFrame();
        }

        pos.y = unactiveYPos;
        rect.anchoredPosition = pos;
    }


}
