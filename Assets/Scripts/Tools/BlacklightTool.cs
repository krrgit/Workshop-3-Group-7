using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlacklightTool : Tool
{
    [SerializeField] private GameObject beam;
    [SerializeField] private GameObject circle;

    public override void Use()
    {
        beam.SetActive(true);
        circle.SetActive(true);       
    }
    
    public override void Stop()
    {
        beam.SetActive(false);
        circle.SetActive(false);
    }
}