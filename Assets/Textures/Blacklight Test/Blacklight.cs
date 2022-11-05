using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Blacklight : MonoBehaviour
{
    public Light2D blacklight;
    public int resolution;

    Material material;
    float outerRadius;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        outerRadius = blacklight.pointLightOuterRadius;
    }

    void Update()
    {
        material.SetTexture("_LightMask", GenerateLightMask());
        Debug.Log("Ran");
    }

    Texture2D GenerateLightMask(){
        Texture2D mask = new Texture2D(resolution, resolution);

        for(int x = 0; x < resolution; x++) {
            for(int y = 0; y < resolution; y++) {

                mask.SetPixel(x, y, Color.white);
            }
        }

        return mask;
    }
}
