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
        GetComponent<SpriteRenderer>().material.SetColor("_Glow", Color.HSVToRGB(0.75f, 1, .8f));
        beam.SetActive(true);
        circle.SetActive(true);       
    }
    
    public override void Stop()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Glow", Color.HSVToRGB(0.75f, 1, 0));
        beam.SetActive(false);
        circle.SetActive(false);
    }
}