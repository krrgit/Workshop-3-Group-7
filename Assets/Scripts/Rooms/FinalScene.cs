using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour {
    [SerializeField] private Animator anim;
    [SerializeField] private CamAimAtTarget cam;
    

    void Start()
    {
        if (ProgressTracker.Instance.SolvedAllPuzzles)
        {
            PlayFinalScene();
        }
    }

    void PlayFinalScene()
    {
        
    }
    
}
