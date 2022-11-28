using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatShadow : MonoBehaviour {
    [SerializeField] private Vector3 drop = new Vector2(0, -0.1f);
    [SerializeField] private Transform cat;

    private void LateUpdate()
    {
        transform.up = cat.up;
        transform.position = cat.position + Vector3.up * drop.y + cat.right * drop.x;
    }
}
