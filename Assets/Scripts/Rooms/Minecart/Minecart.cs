using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour {
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform resetPos;
    [SerializeField] private bool isMoving;
    [SerializeField] private GameObject startInteractor;
    [SerializeField] private AnimateMinecart anim;
    [SerializeField] private GameObject list;

    private Transform parent;

    public void Reset()
    {
        transform.position = resetPos.position;
        transform.up = resetPos.up;
        isMoving = false;
        startInteractor.SetActive(true);
        anim.AnimateReset();
        anim.CatExit();
        SoundManager.Instance.StopmovingCart();
        SoundManager.Instance.PlaywrongPuzzle();
    }

    public void StartCart()
    {
        if (isMoving || !list) return;
        isMoving = true;
        startInteractor.SetActive(false);
        anim.CatEnter();
        SoundManager.Instance.PlaymovingCart();
    }

    public void StopCart()
    {
        if(!isMoving) return;
        
        isMoving = false;
        startInteractor.SetActive(true);
        transform.position = resetPos.position;
        transform.up = resetPos.up;
        if (!list) anim.CatExit();
        SoundManager.Instance.StopmovingCart();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();    
    }

    void Move()
    {
        if (!isMoving) return;
        transform.position += transform.up * speed * Time.deltaTime;
        
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "CartCheckpoint") {
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject checkpointRail = col.transform.parent.gameObject;
            resetPos = checkpointRail.transform.GetChild(1);
            startInteractor = resetPos.GetChild(0).gameObject;
            StopCart();

            parent = this.transform.parent;
            this.transform.parent = checkpointRail.transform;

            StartCoroutine(reachedCheckPoint(checkpointRail.transform, 180, -1));

            
        } else {
            Reset();
        }
    }

    IEnumerator reachedCheckPoint(Transform obj, int rotationStep, int rotationDirection)
    {
        Vector3 currRot = obj.eulerAngles;
        Vector3 targetRot = obj.eulerAngles + new Vector3(0, 0, rotationStep * rotationDirection);

        float dot = Vector3.Dot(obj.up, targetRot);

        while (((int)currRot.z >
            (int)targetRot.z && rotationDirection < 0) || // for clockwise
        ((int)currRot.z < (int)targetRot.z && rotationDirection > 0)) // for anti-clockwise
        {
            currRot.z += rotationDirection * rotationStep * Time.deltaTime;
            obj.eulerAngles = currRot;
            yield return new WaitForEndOfFrame();
        }

        obj.eulerAngles = targetRot;
        StopCart();
        this.transform.parent = parent;
    }
}
