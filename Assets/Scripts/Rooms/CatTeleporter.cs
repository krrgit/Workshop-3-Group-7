using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTeleporter : MonoBehaviour {
    [SerializeField] private Transform exit;
    [SerializeField] private float enterDur = 0.1f;
    [SerializeField] private float exitDur = 0.1f;
    [SerializeField] private CamAimAtTarget cam;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var cat = col.GetComponent<CatAnimController>();
        if (cat)
        {
            StopAllCoroutines();
            StartCoroutine(Animate(cat, col));
        }
    }

    IEnumerator Animate(CatAnimController cat, Collider2D col)
    {
        
        cat.ScriptMove(Vector2.up, enterDur);
        yield return new WaitForSeconds(enterDur+0.2f);
        col.enabled = false;
        cam.ToggleRoomCam(false, null);
        cam.SetTarget(cat.transform);
        cat.transform.position = exit.position;
        cat.ScriptMove(Vector2.left, exitDur);
        yield return new WaitForSeconds(exitDur);
        col.enabled = true;

    }
    
    
}
