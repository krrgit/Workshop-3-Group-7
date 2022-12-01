using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotFinder : MonoBehaviour
{
    [SerializeField] FishingPoleTool pole;
    int fishingAreaCount;

    private void OnTriggerEnter2D(Collider2D noFish)
    {
        if(noFish.gameObject.tag == "FishingSpot")
        {
            pole.SetCorrectSpot(true);
            print("correct spot found");
        }
        else
        {
            pole.SetCorrectSpot(false);
            fishingAreaCount++;
            pole.SetInFishingSpot(true);
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
            fishingAreaCount--;
            if(fishingAreaCount == 0)
            {
                pole.SetInFishingSpot(false);
            }
            else
            {
                pole.SetInFishingSpot(true);
                
            }
        }
    }
}
