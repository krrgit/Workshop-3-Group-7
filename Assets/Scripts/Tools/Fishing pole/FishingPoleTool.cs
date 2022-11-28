using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleTool : Tool
{
    [SerializeField] FishingPoleAnimator anim;

    public override bool Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        print("Use FishingPole!");
        anim.UpdateSprite(PlayerMovement.Instance.FacingDir);
        return true;
    }
    
    public override bool Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        print("Stop using FishingPole");
        return false;
    }
}
