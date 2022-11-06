using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.Rendering.Universal;
using UnityEngine;


public class BlacklightComputeScript : MonoBehaviour
{
    public Light2D blacklight;
    [Range(128, 1024)]public int resolution = 512;
    public bool on = true;

    SpriteRenderer hiddenSprite;
    int width;
    int height;
    float outerRadius;

    Vector3 lightDelta;

    public ComputeShader blacklightCompute;
    RenderTexture mask;

    void Awake()
    {
        hiddenSprite = GetComponent<SpriteRenderer>();
        updateWidthHeightRadiusDelta();
        GenerateMask();
    }

    void Update()
    {
        updateWidthHeightRadiusDelta();
        if(on && shouldRender()) GenerateMask(); 
    }

    void GenerateMask(){
        mask = new RenderTexture(width, height, 16);
        mask.enableRandomWrite = true;
        mask.Create();
         
        blacklightCompute.SetTexture(0, "mask", mask);
        blacklightCompute.SetFloats("deltaPos", new float[]{lightDelta.x, lightDelta.y});
        blacklightCompute.SetFloat("width", width);
        blacklightCompute.SetFloat("height", height);
        blacklightCompute.SetFloat("radius", outerRadius);
        blacklightCompute.Dispatch(0, mask.width/8, mask.height/8, 1);

         hiddenSprite.material.SetTexture("_LightMask", mask);
    }

    bool shouldRender() {
        return lightDelta.sqrMagnitude <= Math.Pow(outerRadius+0.6, 2);
    }

    void updateWidthHeightRadiusDelta() {
        outerRadius = blacklight.pointLightOuterRadius;
        width = Convert.ToInt32(hiddenSprite.size[0]*resolution);
        height = Convert.ToInt32(hiddenSprite.size[1]*resolution);
        lightDelta = blacklight.transform.position - this.transform.position;
    }
}
