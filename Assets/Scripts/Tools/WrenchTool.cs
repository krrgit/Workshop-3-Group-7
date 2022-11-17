using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class WrenchTool : Tool
{
    private Rotator rotate;
    
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