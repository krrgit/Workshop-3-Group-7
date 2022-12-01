using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotFinder : MonoBehaviour
{
    [SerializeField] FishingPoleTool pole;

    private void OnTriggerEnter2D(Collider2D noFish)
    {
        if(noFish.gameObject.tag == "FishingSpot")
        {
            pole.SetCorrectSpot(true);
        }
        else
        {
            pole.SetCorrectSpot(false);
        }
    }

    private void OnTriggerExit2D(Collider2D noFish)
    {
        if(noFish.gameObject.tag == "FishingSpot")
        {
            pole.SetCorrectSpot(false);
        }
        else
        {
            pole.SetCorrectSpot(false);
        }
    }
}
