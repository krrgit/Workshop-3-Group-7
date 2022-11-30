using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Room {
    House,
    Bedroom,
    Garden,
    Aquarium,
    Railroads
}
public class ProgressTracker : MonoBehaviour {
    [SerializeField] private ProgressSO so;

    public static ProgressTracker Instance;

    public bool IsRoomSolved (Room room)
    {
        switch (room)
        {
            case Room.Bedroom:
                return so.bedroomSolved;
            case Room.Railroads:
                return so.railroadsSolved;
            case Room.Garden:
                return so.gardenSolved;
            case Room.Aquarium:
                return so.aquariumSolved;
            default:
                break;
        }

        return false;
    }

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
