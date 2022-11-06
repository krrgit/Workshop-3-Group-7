using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.Rendering.Universal;
using UnityEngine;


public class BackupBlacklightCPU : MonoBehaviour
{
    public Light2D blacklight;
    [Range(32, 512)]public int resolution = 128;
    public bool on = true;

    SpriteRenderer hiddenSprite;
    int width;
    int height;
    float outerRadius;

    Vector3 deltaPos;

    void Awake()
    {
        hiddenSprite = GetComponent<SpriteRenderer>();
        updateWidthHeightRadiusDelta();
        hiddenSprite.material.SetTexture("_LightMask", GenerateLightMask());
    }

    void Update()
    {
        updateWidthHeightRadiusDelta();
        if(on && checkRender()) hiddenSprite.material.SetTexture("_LightMask", GenerateLightMask());
    }

    Texture2D GenerateLightMask(){
        Texture2D mask = new Texture2D(width, height, TextureFormat.RGBA32, false);
        
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                float c = Convert.ToSingle(getColor(x, y));
                mask.SetPixel(x, y, new Color(c, c, c, 1));
            }
        }

        mask.Apply();
        return mask;
    }

    bool checkRender() {
        return deltaPos.sqrMagnitude <= Math.Pow(outerRadius+Math.Max(width, height)/2+0.1, 2);
    }

    double getColor(int x, int y) {
        Vector3 pixelPos = new Vector3(x-width/2, y-height/2, 0);
        pixelPos = new Vector3(pixelPos.x/width, pixelPos.y/height, 0);
        pixelPos -= deltaPos;
        return 1 - Math.Clamp(pixelPos.sqrMagnitude/Math.Pow(outerRadius, 2), 0, 1);
    }

    void updateWidthHeightRadiusDelta() {
        outerRadius = blacklight.pointLightOuterRadius;
        width = Convert.ToInt32(hiddenSprite.size[0]*resolution);
        height = Convert.ToInt32(hiddenSprite.size[1]*resolution);
        deltaPos = blacklight.transform.position - this.transform.position;
    }
}
