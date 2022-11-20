using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool canMove = true;
    [SerializeField] private Transform startPos;
    [SerializeField] private Vector2 startFacingDir = Vector2.down;
    [SerializeField] private float speed = 6;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    
    private Vector2 dir;

    public static PlayerMovement Instance;

    private void OnEnable()
    {
        if (startPos) transform.position = startPos.position;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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
        animator.SetFloat("Horizontal",startFacingDir.x);
        animator.SetFloat("Vertical", startFacingDir.y);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        SetAnimValues();
    }
    
    void FixedUpdate()
    {
        Move();
    }


    void SetAnimValues()
    {
        if(dir.magnitude >= 0.1f)
        {
            animator.SetFloat("Horizontal", dir.x);
            animator.SetFloat("Vertical", dir.y);
        }
    }
    void GetInput()
    {
        if (!canMove) return;
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
