using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomStartPos : MonoBehaviour {
    [SerializeField] private Transform cam;
    [SerializeField] private ProgressTracker prog;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private CatAnimController cat;
    [SerializeField] private Transform[] playerPositions;
    [SerializeField] private Transform[] catPositions;

    [SerializeField] private Vector2[] startDir;
    

    private void OnEnable()
    {
        int room = (int)prog.LastRoom;
        player.SetSpawnPoint(playerPositions[room], startDir[room]);
        cat.SetSpawnPoint(catPositions[room]);
        cam.position = playerPositions[room].position;
    }
}
