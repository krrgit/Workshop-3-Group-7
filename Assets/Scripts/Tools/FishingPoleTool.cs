using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleTool : Tool
{
    public override void Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        print("Use FishingPole!");
    }
    
    public override void Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using FishingPole");
    }
}
