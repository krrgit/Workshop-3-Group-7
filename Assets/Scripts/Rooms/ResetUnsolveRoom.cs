using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUnsolveRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!ProgressTracker.Instance.IsRoomSolved(Room.Aquarium))
        {
            ProgressTracker.Instance.ResetFishCount();
        }
    }
}
