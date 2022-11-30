using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMinecart : MonoBehaviour {
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float clearInterval = 0.1f;

    [Header("Cat In Cart")] 
    [SerializeField] private Sprite cartEmpty;
    [SerializeField] private Sprite cartWithCat;
    [SerializeField] private SpriteRenderer cat;

    public void CatEnter()
    {
        sr.sprite = cartWithCat;
        cat.gameObject.SetActive(false);
    }

    public void CatExit()
    {
        sr.sprite = cartEmpty;
        cat.gameObject.SetActive(true);
    }

    public void AnimateReset()
    {
        StopAllCoroutines();
        StartCoroutine(ResetAnimation());
    }

    IEnumerator ResetAnimation()
    {
        float timer = duration;
        bool spriteOff = true;
        while (timer > 0)
        {
            sr.color = cat.color = spriteOff ? Color.clear : Color.white;
            spriteOff = !spriteOff;
            yield return new WaitForSeconds(clearInterval);
            timer -= clearInterval;
        }
        
        sr.color = cat.color = Color.white;
        
    }
}
