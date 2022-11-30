using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElbowTurn : MonoBehaviour {
    [SerializeField] private Transform exit;
    private void OnTriggerEnter2D(Collider2D col)
    {
        print("Turn");
        col.transform.up = exit.up;
        col.transform.position = transform.position;
    }

    public void SetExit(Transform exit)
    {
        this.exit = exit;
    }
}
