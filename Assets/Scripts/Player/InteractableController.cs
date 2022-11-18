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
    [SerializeField] private int interactAmount = 1;
    

    public bool canInteract = true;
    private int interactCount;

    void Start()
    {
        interactCount = interactAmount;
    }
    
    
    // Returns whether still in interaction or not
    public bool Interact()
    {
        if (interactCount > 0) 
        {
            InteractEvent.Invoke();
            canInteract = false;
            --interactCount;
            return true;
        }
        else
        {
            StopEvent.Invoke();
            Reset();
            return false;
        }
    }

    public void Reset()
    {
        canInteract = true;
        interactCount = interactAmount;
    }
}
