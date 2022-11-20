using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchTool : Tool {
    private RotateController rotator;
    public override bool Use()
    {
        print("Use Wrench!");
        PlayerMovement.Instance.ToggleMove(false);
        if (rotator)
        {
            rotator.RotateObjects();
        }
        // if (InteractController.Instance.interactableExists)
        // {
        //     var rotate = InteractController.Instance.Interactable.GetComponent<RotateController>();
        //
        //     if (rotate)
        //     {
        //         rotate.RotateObjects();
        //     }
        // }

        Stop();
        return false;
    }
    
    public override bool Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using Wrench");
        return false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        rotator = col.GetComponent<RotateController>();
        if (rotator)
        {
            AnimateButtonPrompt.Instance.Animate(col.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (rotator)
        {
            rotator = null;
            AnimateButtonPrompt.Instance.Stop();
        }
    }
}