using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFgBg : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private string background;
    [SerializeField] private string foreground;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().sortingLayerName = player.position.y > transform.position.y ? foreground : background;
    }
}
