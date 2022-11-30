using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetElbowExit : MonoBehaviour {
    [SerializeField] private Transform exit;
    [SerializeField] private ElbowTurn elbow;

    private void OnTriggerEnter2D(Collider2D col)
    {
        elbow.SetExit(exit);
    }
}
