using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchTool : Tool {
    [SerializeField]
    public override bool Use()
    {
        print("Use Wrench!");
        PlayerMovement.Instance.ToggleMove(false);
        if (InteractController.Instance.interactableExists)
        {
            var rotate = InteractController.Instance.Interactable.GetComponent<Rotate>();

            if (rotate)
            {
                rotate.RotateObjects();
            }
        }

        Stop();
        return false;
    }
    
    public override bool Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using Wrench");
        return false;
    }
}