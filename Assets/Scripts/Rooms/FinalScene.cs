using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour {
    [SerializeField] private Transform cineCam;
    [SerializeField] private GameObject endTitle;
    [SerializeField] private LoadNextRoom load;
    [SerializeField] private float camSpeed = 0.5f;
    [SerializeField] private bool play;
    [SerializeField] private GameObject finalObjects;
    [SerializeField] private Image finText;

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
        DisableObjects();
        AnimateTransitionStencil.Instance.UpdateStencil(0);
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

        Color alpha = Color.white;
        alpha.a = 0;

        while (alpha.a < 1)
        {
            finText.color = alpha;
            alpha.a += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(3);
        
        load.LoadMainMenu();
    }
    
}
