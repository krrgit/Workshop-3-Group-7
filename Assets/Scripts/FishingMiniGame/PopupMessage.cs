using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupMessage : MonoBehaviour
{
    public GameObject popupMessage;
    
    private void OnTriggerEnter2D(Collider2D noFish)
    {
        if(noFish.gameObject.tag == "FishingSpot")
        {
            print("fishing spot found");
        }
        else
        {
            popupMessage.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D noFish)
    {
        if(noFish.gameObject.tag == "FishingSpot")
        {

        }
        else
        {
            popupMessage.gameObject.SetActive(false);
        }
    }
}
