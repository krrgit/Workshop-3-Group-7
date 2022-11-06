using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.Rendering.Universal;
using UnityEngine;


public class BlacklightComputeScript : MonoBehaviour
{
    public Light2D blacklight;
    [Range(32, 1024)]public int resolution = 512;
    public bool on = true;

    SpriteRenderer hiddenSprite;
    int width;
    int height;
    float outerRadius;

    Vector3 lightDelta;

    public ComputeShader blacklightCompute;
    public RenderTexture renderTexture;

    void Awake()
    {
        hiddenSprite = GetComponent<SpriteRenderer>();
        setWidthHeightRadius();
        GenerateLightMask();

        hiddenSprite.material.SetTexture("_LightMask", renderTexture);
    }

    void Update()
    {
        setWidthHeightRadius();
        GenerateLightMask();
        if(on && checkRender()) hiddenSprite.material.SetTexture("_LightMask", renderTexture);
    }

    void GenerateLightMask(){
        renderTexture = new RenderTexture(width, height, 1);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();
         
         blacklightCompute.SetTexture(0, "mask", renderTexture);
         blacklightCompute.SetFloats("deltaPos", new float[]{lightDelta.x, lightDelta.y});
         blacklightCompute.SetFloat("width", width);
         blacklightCompute.SetFloat("height", height);
         blacklightCompute.SetFloat("radius", outerRadius);
         blacklightCompute.Dispatch(0, renderTexture.width/8, renderTexture.height/8, 1);
    }

    bool checkRender() {
        return lightDelta.sqrMagnitude <= Math.Pow(outerRadius+0.6, 2);
    }

    void setWidthHeightRadius() {
        outerRadius = blacklight.pointLightOuterRadius;
        width = Convert.ToInt32(hiddenSprite.size[0]*resolution);
        height = Convert.ToInt32(hiddenSprite.size[1]*resolution);
        lightDelta = blacklight.transform.position - this.transform.position;
    }
}
