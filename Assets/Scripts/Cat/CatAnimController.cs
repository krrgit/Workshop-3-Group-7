using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CatAnimController : MonoBehaviour {
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Animator anim;
   [SerializeField] private CatFollowPlayer follow;
   [SerializeField] private bool canMove;
   [SerializeField] private bool isControlled;
   [SerializeField] private bool followPlayer;
   [SerializeField] private float moveSpeed = 1;
   [SerializeField] private float turnSpeed = 1;
   [SerializeField] private bool isSleeping;
   [SerializeField] private Transform spawnPoint;

   private bool turnClockwise;
   private bool turnCounter;
   private bool moveForward;

   private Vector2 dir;
   private Quaternion toRot;
   private float rotDiff;

   private Vector2 tempDir;

   private bool isCheckingFollow;

   public static CatAnimController Instance;

   private bool isMeowing;

   public void SetDefaultFollowValues()
   {
      anim.Play("Cat_Idle");
      isSleeping = false;
      canMove = true;
      followPlayer = true;
   }

   public void PlaySleepAnim()
   {
      anim.Play("Cat_Sleep");
   }

   public void SetSpawnPoint(Transform t)
   {
      spawnPoint = t;
      
      transform.position = spawnPoint.position;
      transform.up = spawnPoint.right;

      follow.CreateStartPoints();
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

   public void ToggleControl(bool control)
   {
      if (isSleeping)
      {
         anim.Play("Cat_Idle");
         isSleeping = false;
      }
      
      isControlled = !control;
      if (!isControlled)
      {
         StartCoroutine(IFollowCheck());
      }
      else
      {
         followPlayer = false;
      }
   }

   void Start()
   {
      follow.CreateStartPoints();
   }

   public void ScriptMove(Vector2 _dir, float dur)
   {
      StopAllCoroutines();
      StartCoroutine(IScriptMove(_dir, dur));
   }

   IEnumerator IScriptMove(Vector2 _dir, float dur)
   {
      isControlled = false;

      while (dur > 0)
      {
         dir = _dir;
         dur -= Time.deltaTime;
         yield return new WaitForEndOfFrame();
      }
      
      dir = Vector2.zero;
      isControlled = true;
   }
   
   private void OnEnable()
   {
      if (spawnPoint)
      {
         transform.position = spawnPoint.position;
         transform.up = spawnPoint.right;
      }
   }

   void Update()
   {
      FollowCheck();
      GetInput();
      FollowInput();
      ButtonCommands();
      SetTurnBoolValues();
      
      SetAnimVariables();
      Move();
      Rotate();
   }

   void FollowCheck()
   {
      if (isControlled || !canMove) return;

      if (!isCheckingFollow)
      {
         StartCoroutine(IFollowCheck());
      }
   }

   IEnumerator IFollowCheck()
   {
      isCheckingFollow = true;
      RaycastHit2D hit;
      while (!followPlayer)
      {

         hit = Physics2D.Raycast((Vector3)follow.FollowPoint,  transform.position - (Vector3)follow.FollowPoint);
         if (hit.collider.gameObject == gameObject)
         {
            followPlayer = true;
         }
         yield return new WaitForSeconds(0.35f);
      }
      isCheckingFollow = true;
   }

   void FollowInput()
   {
      if (!followPlayer || isControlled) return;

      tempDir = follow.FollowPoint - (Vector2)transform.position;
      dir = tempDir.normalized;
      if (tempDir.magnitude < 0.1f)
      {
         dir = Vector2.zero;
      }
   }

   void ButtonCommands()
   {
      if (!isControlled) return;

      if (Input.GetButtonDown("CycleTool"))
      {
         SwitchPlayerController.Instance.Switch(true);
      }

      if (Input.GetButtonDown("Interact"))
      {
         
         if (!isMeowing)
         {
            StartCoroutine(PlayMeow());
            SoundManager.Instance.Playmeow();
         }
      }
   }

   IEnumerator PlayMeow()
   {
      isMeowing = true;
      // INSERT PLAY FUNCTION CALL HERE
      yield return new WaitForSeconds(1);
      isMeowing = false;
   }
   
   

   void GetInput()
   {
      if (!isControlled) return;

         dir.x = Input.GetAxisRaw("Horizontal");
      dir.y = Input.GetAxisRaw("Vertical");
      dir = Vector2.ClampMagnitude(dir,1);
   }

   void SetTurnBoolValues()
   {
      rotDiff = Mathf.Abs(transform.rotation.z) - Mathf.Abs(toRot.z);
     // print("z diff: " + rotDiff );
      if (Mathf.Abs(rotDiff) > 0.1f)
      {
         turnCounter = rotDiff < 0 ? true : false;
         turnClockwise = !turnCounter;
      }
      else
      {
         turnCounter = turnClockwise = false;
      }

      moveForward = (dir.magnitude != 0);
   }

   
   void SetAnimVariables()
   {
      anim.SetBool("turnClockwise", turnClockwise);
      anim.SetBool("turnCounter", turnCounter);
      anim.SetBool("moveForward", moveForward);
   }

   void Move()
   {
      rb.position +=  moveSpeed * Time.deltaTime * dir;
   }

   void Rotate()
   {
      if (dir.magnitude == 0) return;
      toRot = Quaternion.LookRotation(Vector3.forward, dir);
      transform.rotation = Quaternion.Slerp(transform.rotation, toRot, Time.deltaTime * turnSpeed);
      
      //transform.Rotate(0,0,turnDir * turnSpeed * Time.deltaTime);
   }

   void SetTurnValues(int direction)
   {
      if (direction == 1)
      {
         turnClockwise = false;
         turnCounter = true;
      } else if (direction == -1)
      {
         turnCounter = false;
         turnClockwise = true;
      }
      else
      {
         turnCounter = false;
         turnClockwise = false;
      }
   }
}
