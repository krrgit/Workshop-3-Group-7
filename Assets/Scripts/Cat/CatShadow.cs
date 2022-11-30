using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatShadow : MonoBehaviour {
    [SerializeField] private Vector3 drop = new Vector2(0, -0.1f);
    [SerializeField] private Transform cat;
    [SerializeField] private Vector3 activeSize = new Vector3(1.75f, 1, 1);

    private void Awake()
    {
        transform.localScale = activeSize;
    }

    private void LateUpdate()
    {
        transform.up = cat.up;
        transform.position = cat.position + Vector3.up * drop.y + cat.right * drop.x;
    }
}
