using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool canMove = true;
    public bool isControlling = true;
    [SerializeField] private Transform startPos;
    [SerializeField] private Vector2 startFacingDir = Vector2.down;
    [SerializeField] private float speed = 6;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private Vector2 dir;

    private Vector2 facingDir;

    public static PlayerMovement Instance;
    
    public void SetSpawnPoint(Transform t, Vector2 dir)
    {
        startPos = t;
        startFacingDir = dir;
    }


    public void ToggleControl(bool state)
    {
        StartCoroutine(WaitForControl(state));
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

    public Vector2 FacingDir
    {
        get { return facingDir; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("Horizontal",startFacingDir.x);
        animator.SetFloat("Vertical", startFacingDir.y);
        
        if (startPos) transform.position = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isControlling) return;
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
            facingDir = dir;
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

    IEnumerator WaitForControl(bool state)
    {
        yield return new WaitForEndOfFrame();
        isControlling = state;
    }
}
