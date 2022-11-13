using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTransitionStencil : MonoBehaviour {
    [SerializeField] private RectTransform rect;
    [SerializeField] private float duration = 1;

    private Vector2 size;
    
    void Start()
    {
        size = rect.sizeDelta;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnimateEnter();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AnimateExit();
        }
    }

    public void AnimateEnter()
    {
        StopAllCoroutines();
        StartCoroutine(IOpen());
    }
    
    public void AnimateExit()
    {
        StopAllCoroutines();
        StartCoroutine(IClose());
    }

    IEnumerator IOpen()
    {
        rect.sizeDelta = Vector2.zero;
        float scale = 0;

        while (scale < 1)
        {
            rect.sizeDelta = size * scale;
            scale += Time.deltaTime / duration;
            yield return new WaitForEndOfFrame();
        }

        rect.sizeDelta = size;
    }
    
    IEnumerator IClose()
    {
        rect.sizeDelta = size;
        float scale = 1;

        while (scale > 0)
        {
            rect.sizeDelta = size * scale;
            scale -= Time.deltaTime / duration;
            yield return new WaitForEndOfFrame();
        }

        rect.sizeDelta = Vector2.zero;
    }

}
