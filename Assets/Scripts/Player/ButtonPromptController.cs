using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPromptController : MonoBehaviour {
    [SerializeField] private AnimateButtonPrompt prompt;

    private InteractableController interactable;

    private bool isPrompted;
    private void OnTriggerEnter2D(Collider2D col)
    {
        interactable = col.GetComponent<InteractableController>();
        prompt.Animate(col.transform,1);
        isPrompted = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isPrompted && interactable.canInteract || !interactable.canInteract && !isPrompted) return;
        
        if (!interactable.canInteract && isPrompted)
        {
            Stop();
        }
        else
        {
            prompt.Animate(other.transform,1);
            isPrompted = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Stop();
    }

    public void Stop()
    {
        prompt.Stop();
        isPrompted = false;
    }
}
