using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMinecart : MonoBehaviour {
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float clearInterval = 0.1f;
    

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
            sr.color = spriteOff ? Color.clear : Color.white;
            yield return new WaitForSeconds(clearInterval);
            timer -= clearInterval;
        }
        
        sr.color = Color.white;
    }
}
