using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateButtonPrompt : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Transform target;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private RectTransform rect;

    private float defaultSize;

    void Start()
    {
        defaultSize = rect.localScale.x;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (target) transform.position = target.position;
    }

    public void Animate(Transform t)
    {
        gameObject.SetActive(true);
        target = t;
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
