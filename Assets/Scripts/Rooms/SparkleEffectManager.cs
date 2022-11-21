using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleEffectManager : MonoBehaviour {
    [SerializeField] private ParticleSystem sparkles;

    private void OnTriggerEnter2D(Collider2D col)
    {
        sparkles.Play();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        sparkles.Stop();
    }
}
