using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSolvesPuzzle : MonoBehaviour {
    [SerializeField] private Room roomToSolve;
    private void OnTriggerEnter2D(Collider2D col)
    {
        ProgressTracker.Instance.SolvePuzzle(roomToSolve);
        Destroy(gameObject);
    }
}
