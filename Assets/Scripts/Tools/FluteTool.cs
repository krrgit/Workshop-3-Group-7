using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteTool : Tool {
    [SerializeField] private AnimateMusicUI musicUI;
    [SerializeField] private NotePlacer notePlacer;
    public override void Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        musicUI.Animate(true);
        print("Use Flute!");
    }
    
    public override void Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        musicUI.Animate(false);
        print("Stop using Flute");
    }
}
