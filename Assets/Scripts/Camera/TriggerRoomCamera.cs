using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoomCamera : MonoBehaviour {
    [SerializeField] private CamAimAtTarget cam;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            cam.ToggleRoomCam(true, transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cam.ToggleRoomCam(false, null);
        }
    }
}
