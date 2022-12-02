using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour {
    [SerializeField] private Transform cineCam;
    [SerializeField] private GameObject endTitle;
    [SerializeField] private LoadNextRoom load;
    [SerializeField] private float camSpeed = 0.5f;
    [SerializeField] private bool play;
    [SerializeField] private GameObject finalObjects;

    [SerializeField] private GameObject[] objects;

    private bool canExit;

    void Start()
    {
        if (play || ProgressTracker.Instance.SolvedAllPuzzles)
        {
            PlayFinalScene();
        }
    }

    private void Update()
    {
        
    }

    void PlayFinalScene()
    {
        AnimateTransitionStencil.Instance.UpdateStencil(0);
        DisableObjects();
        StartCoroutine(FinalSequence());
    }

    void DisableObjects()
    {
        finalObjects.SetActive(true);
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
        
    }

    IEnumerator FinalSequence()
    {
        // Animate Camera
        cineCam.localPosition = new Vector3(0, -4, -10);
        float yPos = -5;

        while (yPos < 0)
        {
            cineCam.localPosition = new Vector3(0, yPos, -10);
            yPos += Time.deltaTime * camSpeed;
            yield return new WaitForEndOfFrame();
        }
        cineCam.localPosition = new Vector3(0, 0, -10);
        
        endTitle.SetActive(true);
        yield return new WaitForSeconds(4);
        
        load.LoadMainMenu();
    }
    
}
