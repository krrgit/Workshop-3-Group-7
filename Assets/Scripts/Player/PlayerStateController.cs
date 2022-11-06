using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    Idle,
    Wrench,
    Fishing,
    Blacklight,
    Flute
}

public class PlayerStateController : MonoBehaviour {
    
    private PlayerState state;

    public void EnterState(PlayerState newState)
    {
        state = newState;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
