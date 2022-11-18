using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchTool : Tool
{
    public override void Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        print("Use Wrench!");
    }
    
    public override void Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using Wrench");
    }
}