using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public class AnimateTransitionStencil : MonoBehaviour {
    [SerializeField] private Image stencil;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private RectTransform rect;
    [SerializeField] private float duration = 1;
    [SerializeField] private float startDelay = 0.4f;

    private Vector2 size;

    public static AnimateTransitionStencil Instance;
    
    

    public void UpdateStencil(int index)
    {
        stencil.sprite = sprites[index];
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        size = rect.sizeDelta;
        StartCoroutine(IDelay());
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

    public float AnimateEnter()
    {
        StopAllCoroutines();
        StartCoroutine(IOpen());
        return duration;
    }
    
    public float AnimateExit()
    {
        StopAllCoroutines();
        StartCoroutine(IClose());
        return duration;
    }

    IEnumerator IDelay()
    {
        rect.sizeDelta = Vector2.zero;
        yield return new WaitForSeconds(startDelay);
        AnimateEnter();
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
