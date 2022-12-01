using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNextRoom : MonoBehaviour {
    [SerializeField] private Room thisRoom;
    [SerializeField] private Room nextRoom;
    [SerializeField] private AnimateTransitionStencil stencilAnim;
    private bool loading;

    int Stencil
    {
        get { return Mathf.Max((int)thisRoom - 1, (int)nextRoom-1); }
    }

    private void Start()
    {
        if (ProgressTracker.Instance.LastRoom == nextRoom)
        {
            stencilAnim.UpdateStencil(Stencil);
        }
        
    }
    
    public Room GetRoom()
    {
        return thisRoom;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (loading) return;
            SoundManager.Instance.PlaydoorOpen();
            PlayerMovement.Instance.canMove = false;
            AnimateTransitionStencil.Instance.UpdateStencil(Stencil);
            float dur = AnimateTransitionStencil.Instance.AnimateExit();
            StartCoroutine(LoadNextScene(dur));
        }
    }

    IEnumerator LoadNextScene(float delay)
    {
        loading = true;
        ProgressTracker.Instance.SetLastRoom(thisRoom);
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene((int)nextRoom);
        loading = false;
    }
}
