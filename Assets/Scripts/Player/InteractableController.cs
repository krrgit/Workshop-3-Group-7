/** Interactable Controller
 * This class is to turn an object into an interactable
 * The object needs to be on the interact layer in order to work (or its own layer that can collide with Interact layer)
 * Two events:
 * InteractEvent is when the player goes up to an Interactable and interacts with it
 * StopEvent is when the player presses the button to exit the interaction
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableController : MonoBehaviour {
    public UnityEvent InteractEvent;
    public UnityEvent StopEvent;

    public bool canInteract = true;

    public void Interact()
    {
        if (canInteract)
        {
            InteractEvent.Invoke();
            canInteract = false;
        }
        else
        {
            StopEvent.Invoke();
            Reset();
        }
    }

    public void Reset()
    {
        canInteract = true;
    }
}
