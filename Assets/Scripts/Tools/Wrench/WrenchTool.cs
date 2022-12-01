using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchTool : Tool {
    [SerializeField] private RotateController rotator;
    public override bool Use()
    {
        if (rotator)
        {
            print("Use Wrench!");
            PlayerMovement.Instance.ToggleMove(false);
            float waitTime = rotator.RotateObjects();
            StartCoroutine(Wait(waitTime));
        }

        return false;
    }
    
    public override bool Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using Wrench");
        return false;
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Stop();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        rotator = col.GetComponent<RotateController>();
        if (rotator)
        {
            AnimateButtonPrompt.Instance.Animate(col.transform,0);
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