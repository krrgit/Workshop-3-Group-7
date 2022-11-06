using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.Rendering.Universal;
using UnityEngine;


public class Blacklight : MonoBehaviour
{
    public Light2D blacklight;
    [Range(32, 512)]public int resolution;
    public bool on;

    SpriteRenderer hiddenSprite;
    int width;
    int height;
    float outerRadius;

    void Awake()
    {
        hiddenSprite = GetComponent<SpriteRenderer>();
        setVars();

        hiddenSprite.material.SetTexture("_LightMask", GenerateLightMask());
    }

    void Update()
    {
        setVars();
        if(on && checkRender()) hiddenSprite.material.SetTexture("_LightMask", GenerateLightMask());
    }

    Texture2D GenerateLightMask(){
        Texture2D mask = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Vector3 deltaPos = blacklight.transform.position - this.transform.position;

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                float c = 1 - Convert.ToSingle(checkPos(x, y, deltaPos));
                mask.SetPixel(x, y, new Color(c, c, c, 1));
            }
        }

        mask.Apply();
        return mask;
    }

    bool checkRender() {
        Vector3 deltaPos = blacklight.transform.position - this.transform.position;
        return deltaPos.sqrMagnitude <= Math.Pow(outerRadius+0.5, 2);
    }

    double checkPos(int x, int y, Vector3 deltaPos) {
        Vector3 pixelPos = new Vector3(x-width/2, y-height/2, 0);
        pixelPos = new Vector3(pixelPos.x/width, pixelPos.y/height, 0);
        pixelPos -= deltaPos;

        return Math.Max(0, Math.Min(1, pixelPos.sqrMagnitude/Math.Pow(outerRadius, 2)));
    }

    void setVars() {
        outerRadius = blacklight.pointLightOuterRadius;
        width = Convert.ToInt32(hiddenSprite.size[0]*resolution);
        height = Convert.ToInt32(hiddenSprite.size[1]*resolution);
    }
}
