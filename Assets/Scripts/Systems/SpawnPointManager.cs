using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour {
    [Header("Bedroom")]
    [SerializeField] private Transform bedPlayerSpawn;
    [SerializeField] private Transform bedCatSpawn;
    [SerializeField] private Vector2 bedFacingDir;
    [Header("Railroads")]
    [SerializeField] private Transform railroadsPlayerSpawn;
    [SerializeField] private Transform railroadsCatSpawn;
    [SerializeField] private Vector2 railroadsFacingDir;
    [Header("Garden")]
    [SerializeField] private Transform gardenPlayerSpawn;
    [SerializeField] private Transform gardenCatSpawn;
    [SerializeField] private Vector2 gardenFacingDir;
    [Header("Aquarium")]
    [SerializeField] private Transform aquariumPlayerSpawn;
    [SerializeField] private Transform aquariumCatSpawn;
    [SerializeField] private Vector2 aquariumFacingDir;


    void OnEnable()
    {
        SetPlayerSpawn();
    }

    void SetPlayerSpawn()
    {
        switch (ProgressTracker.Instance.LastRoom)
        {
            case Room.Bedroom:
                PlayerMovement.Instance.SetSpawnPoint(bedCatSpawn, bedFacingDir);
                CatAnimController.Instance.SetSpawnPoint(bedCatSpawn);
                break;
            case Room.Railroads:
                PlayerMovement.Instance.SetSpawnPoint(railroadsPlayerSpawn, railroadsFacingDir);
                CatAnimController.Instance.SetSpawnPoint(railroadsCatSpawn);
                break;
            case Room.Garden:
                PlayerMovement.Instance.SetSpawnPoint(gardenPlayerSpawn, gardenFacingDir);
                CatAnimController.Instance.SetSpawnPoint(gardenCatSpawn);
                break;
            case Room.Aquarium:
                PlayerMovement.Instance.SetSpawnPoint(aquariumPlayerSpawn, aquariumFacingDir);
                CatAnimController.Instance.SetSpawnPoint(aquariumCatSpawn);
                break;
            default:
                break;
        }
    }


}
