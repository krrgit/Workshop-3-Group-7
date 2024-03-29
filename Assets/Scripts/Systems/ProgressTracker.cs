using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Room {
    House,
    Bedroom,
    Garden,
    Aquarium,
    Railroads
}
public class ProgressTracker : MonoBehaviour {
    [SerializeField] private ProgressSO so;

    public UnityEvent SolvedBedroom;
    public UnityEvent SolvedAquarium;
    public UnityEvent SolvedGarden;
    public UnityEvent SolvedRailroads;

    
    public static ProgressTracker Instance;

    public bool SolvedAllPuzzles
    {
        get { return so.aquariumSolved && so.bedroomSolved && so.gardenSolved && so.railroadsSolved; }
    }

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

    public int FishCaught
    {
        get { return so.listPieces; }
    }

    public void Reset()
    {
        so.lastRoom = Room.Bedroom;
        so.aquariumSolved = so.bedroomSolved = so.gardenSolved = so.railroadsSolved = false;
        so.listPieces = 0;
    }

    public void ResetFishCount()
    {
        so.listPieces = 0;
    }

    public void SetLastRoom(Room room)
    {
        so.lastRoom = room;
    }

    public void UpdateFishCaught()
    {
        // Already caught all fish, exit
        if (so.listPieces >= 4) return;
        
        ++so.listPieces;
        if (so.listPieces == 4)
        {
            SoundManager.Instance.PlaypuzzleSolved();
            SolvePuzzle(Room.Aquarium);
        }
    }
    
    public void SolvePuzzle(Room solvedRoom)
    {
        switch (solvedRoom)
        {
            case Room.Bedroom:
                so.bedroomSolved = true;
                SolvedBedroom.Invoke();
                break;
            case Room.Railroads:
                so.railroadsSolved = true;
                SolvedRailroads.Invoke();
                break;
            case Room.Garden:
                so.gardenSolved = true;
                SolvedGarden.Invoke();
                break;
            case Room.Aquarium:
                so.aquariumSolved = true;
                SolvedAquarium.Invoke();
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
