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

    public Room lastRoom = Room.Bedroom;
    public bool bedroomSolved;
    public bool railroadsSolved;
    public bool gardenSolved;
    public bool aquariumSolved;

    public static ProgressTracker Instance;
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void SolvePuzzle(Room solvedRoom)
    {
        switch (solvedRoom)
        {
            case Room.Bedroom:
                bedroomSolved = true;
                break;
            case Room.Railroads:
                railroadsSolved = true;
                break;
            case Room.Garden:
                gardenSolved = true;
                break;
            case Room.Aquarium:
                aquariumSolved = true;
                break;
            default:
                break;
        }
    }
}
