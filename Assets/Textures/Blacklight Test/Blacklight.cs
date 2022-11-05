using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;


public class Blacklight : MonoBehaviour
{
    public Light2D blacklight;
    [Range(16, 512)]public int resolution;
    public bool on;

    Material material;
    float outerRadius;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        outerRadius = blacklight.pointLightOuterRadius;
    }

    void Update()
    {
        if(on) material.SetTexture("_LightMask", GenerateLightMask());
    }

    Texture2D GenerateLightMask(){
        Texture2D mask = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        float radius = outerRadius*resolution;

        for(int x = 0; x < resolution; x++) {
            for(int y = 0; y < resolution; y++) {
                if(checkPos(x, y, radius)){
                    mask.SetPixel(x, y, Color.white);
                } else {
                    mask.SetPixel(x, y, Color.black);
                }
            }
        }

        mask.Apply();

        return mask;
    }

    bool checkPos(int x, int y, float radius) {
        float xDelta = x - blacklight.transform.position.x;
        float yDelta = y - blacklight.transform.position.y;

        if ((xDelta*xDelta + yDelta*yDelta) <= radius*radius) {
            return true;
        }

        return false;
    }
}
