using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNextRoom : MonoBehaviour {
    [SerializeField] private Room thisRoom;
    [SerializeField] private int stencil;
    [SerializeField] private int nextScene;
    private bool loading;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (loading) return;
            
            PlayerMovement.Instance.canMove = false;
            AnimateTransitionStencil.Instance.UpdateStencil(stencil);
            float dur = AnimateTransitionStencil.Instance.AnimateExit();
            StartCoroutine(LoadNextScene(dur));
        }
    }

    IEnumerator LoadNextScene(float delay)
    {
        loading = true;
        if (ProgressTracker.Instance) ProgressTracker.Instance.lastRoom = thisRoom;
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(nextScene);
        loading = false;
    }
}
