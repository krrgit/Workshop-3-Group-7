using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateProgressTracker : MonoBehaviour {
    [SerializeField] private Room solvedRoom;
    private void OnTriggerEnter2D(Collider2D col)
    {
        ProgressTracker.Instance.SolvePuzzle(solvedRoom);
        print("Solved " + solvedRoom.ToString() + "!");
        Destroy(gameObject);
    }
}
