using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProgressSO", menuName = "ScriptableObjects/Progress", order = 1)]
public class ProgressSO : ScriptableObject
{
    public Room lastRoom = Room.Bedroom;
    public bool bedroomSolved;
    public bool railroadsSolved;
    public bool gardenSolved;
    public bool aquariumSolved;

    public int listPieces = 0;
    
}
