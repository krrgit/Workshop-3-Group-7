using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteTool : Tool
{
    public override void Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        print("Use Flute!");
    }
    
    public override void Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using Flute");
    }
}
