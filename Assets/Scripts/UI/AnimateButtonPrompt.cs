using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateButtonPrompt : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Transform target;
    [SerializeField] private float targetPadding = 1;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private RectTransform rect;

    private float defaultSize;

    public static AnimateButtonPrompt Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        defaultSize = rect.localScale.x;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (target) transform.position = target.position + Vector3.up * targetPadding;
    }

    public void Animate(Transform t, float padding)
    {
        gameObject.SetActive(true);
        target = t;
        targetPadding = padding;
        StopAllCoroutines();
        StartCoroutine(IAnimate());
    }

    public void Stop()
    {
        gameObject.SetActive(false);
        StopAllCoroutines();
        rect.localScale = Vector2.zero;
    }
    
    // Open Text Bubble
    IEnumerator IAnimate()
    {
        float timer = 0;
        yield return new WaitForEndOfFrame();

        while (timer < 1)
        {
            rect.localScale = Vector2.one * animCurve.Evaluate(timer) * defaultSize;
            timer += Time.deltaTime/ duration;
            yield return new WaitForEndOfFrame();
        }

        rect.localScale = Vector2.one * defaultSize;
    }
}
