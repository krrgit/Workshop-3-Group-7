using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Room {
    Bedroom,
    Railroads,
    Garden,
    Aquarium,
    House
}
public class ProgressTracker : MonoBehaviour {
    [SerializeField] private ProgressSO so;

    public static ProgressTracker Instance;

    public Room LastRoom
    {
        get { return so.lastRoom; }
    }

    public void SetLastRoom(Room room)
    {
        so.lastRoom = room;
    }


    public void SolvePuzzle(Room solvedRoom)
    {
        switch (solvedRoom)
        {
            case Room.Bedroom:
                so.bedroomSolved = true;
                break;
            case Room.Railroads:
                so.railroadsSolved = true;
                break;
            case Room.Garden:
                so.gardenSolved = true;
                break;
            case Room.Aquarium:
                so.aquariumSolved = true;
                break;
            default:
                break;
        }
    }
    
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }
}
