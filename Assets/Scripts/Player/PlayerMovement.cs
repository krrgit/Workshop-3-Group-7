using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool canMove = true;
    [SerializeField] private float speed = 6;
    [SerializeField] private Rigidbody2D rb;
    
    private Vector2 dir;

    public void ToggleMove(bool toggle)
    {
        canMove = toggle;
    }

    public Vector2 GetDir()
    {
        return dir;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        dir = Vector2.ClampMagnitude(dir,1);
    }

    void Move()
    {
        if (!canMove) return;
        rb.position += dir * speed * Time.fixedDeltaTime;
    }
}
