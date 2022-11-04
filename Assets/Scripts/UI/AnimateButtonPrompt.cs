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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Animate();
        }
    }

    public void Animate()
    {
        StopAllCoroutines();
        if (target) transform.position = target.position;
        StartCoroutine(IAnimate());
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
