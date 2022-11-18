/** Interact Controller
 * This class is added to the child of the player with a trigger collider.
 * This class is in charge of calling the interactable class's function
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractController : MonoBehaviour {
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private float distance = 1;

    private InteractableController currInteractable;
    private bool inInteraction;
    
    public  bool interactableExists;

    public static InteractController Instance;

    public InteractableController Interactable
    {
        get
        {
            return currInteractable;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
        UpdatePosition();
    }

    void Controls()
    {
        if (!interactableExists) return;
        if (Input.GetButtonDown("Interact"))
        {
            inInteraction = currInteractable.Interact();
            movement.canMove = !inInteraction;
        }
    }

    void UpdatePosition()
    {
        if (movement.GetDir().magnitude == 0 || !movement.canMove) return;

        transform.localPosition = movement.GetDir() * distance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        currInteractable = col.GetComponent<InteractableController>();
        interactableExists = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currInteractable.gameObject == other.gameObject)
        {
            currInteractable = null;
            interactableExists = false;
        }
    }
}
