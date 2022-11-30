using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrier : MonoBehaviour {
    [SerializeField] private LoadNextRoom load;
    public void Destroy()
    {
        ProgressTracker.Instance.SolvePuzzle(load.GetRoom());
        Destroy(gameObject);
    }
}
