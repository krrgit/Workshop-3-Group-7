using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomStateController : MonoBehaviour {
    [SerializeField] private GameObject barrier;
    [SerializeField] private ProgressTracker progress;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private CatAnimController cat;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform catSpawnPoint;

    private void OnEnable()
    {
        if (progress.IsRoomSolved(Room.Bedroom))
        {
            Destroy(barrier);
            player.SetSpawnPoint(spawnPoint,Vector2.left);
            cat.SetSpawnPoint(catSpawnPoint);
            cat.SetDefaultFollowValues();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
