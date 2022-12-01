using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.Rendering.Universal;
using UnityEngine;

public class BlacklightObject : MonoBehaviour
{
    [SerializeField] private Light2D blacklight;                        //Light Object
    [SerializeField] [Range(128, 1024)] public int resolution = 512;    //Texture Resolution

    private SpriteRenderer hiddenSprite;                                //sprite to be hidden
    private int width;                                                  //width of texture to make
    private int height;                                                 //height of texture to make
    private float outerRadius;                                          //radius of blacklight

    [SerializeField] private ComputeShader blacklightCompute;           //compute shader for texture
    [SerializeField] private RenderTexture mask;                        //texture mask generated to put over sprite

    private Vector3 lightDelta;                                         //relative position of light to hiddenSprite
    private Vector3 previousDelta;                                      //last coordinate that texture was rendered from
    private bool active;

    void Awake() {
        hiddenSprite = GetComponent<SpriteRenderer>();  //set the sprite to the objects SpriteRenderer
        updateWidthHeightRadiusDelta();                 //set all necessary variables for mask generation
        GenerateMask();                                 //generate mask
    }

    void Update() {
        updateWidthHeightRadiusDelta();                                                                     //set all necessary variables for mask generation
        if(blacklight.gameObject.activeSelf && (lightDelta != previousDelta || !active)) {                  //check if the mask should be rendered
            GenerateMask();                                                                                 //generate the mask
            previousDelta = lightDelta;                                                                     //store the coordinate of the last generated texture
            active = true;                                                                                  //set the texture bool to be on
        } else if(!blacklight.gameObject.activeSelf && active) {                                            //if the blacklight object was disabled but the texture is on
            hiddenSprite.material.SetTexture("_LightMask", Texture2D.blackTexture);                         //set the texture to be invisible
            active = false;                                                                                 //set the texture bool to off
        }
    }

    void GenerateMask() {
        mask = new RenderTexture(width, height, 16);                                        //set new mask
        mask.enableRandomWrite = true;                                                      //allow mask to be written
        mask.Create();                                                                      //create mask

        blacklightCompute.SetTexture(0, "mask", mask);                                      //set mask to be edited by the compute shader
        blacklightCompute.SetFloats("deltaPos", new float[]{lightDelta.x, lightDelta.y});   //set the relative position of the light in the shader
        blacklightCompute.SetFloat("width", width);                                         //set the width in the shader
        blacklightCompute.SetFloat("height", height);                                       //set the height in the shader
        blacklightCompute.SetFloat("radius", outerRadius);                                  //set the radius in the shader
        blacklightCompute.SetFloat("resolution", resolution);                               //set the resolution in the shader
        blacklightCompute.Dispatch(0, mask.width/8, mask.height/8, 1);                      //dispatch the compute shader to generate the texture

        hiddenSprite.material.SetTexture("_LightMask", mask);                               //update the texture
    }

    bool inRadius() {
        return lightDelta.sqrMagnitude <= Math.Pow(outerRadius+Math.Max(hiddenSprite.size[0], hiddenSprite.size[1]), 2);   //check if the light is within range to be rendered (using square magnitude because sqrt takes a long time)
    }

    void updateWidthHeightRadiusDelta() {
        outerRadius = blacklight.pointLightOuterRadius;                                 //set the outer radius
        width = Convert.ToInt32(hiddenSprite.size[0]*resolution);           //set the width based on the relative size and the resolution
        height = Convert.ToInt32(hiddenSprite.size[1]*resolution);          //set the height based on the relative size and the resolution
        lightDelta = blacklight.transform.position - this.transform.position;           //find the position of the light relative to the hiddenSprite
    }
}
