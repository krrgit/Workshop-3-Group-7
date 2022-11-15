using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CatAnimController : MonoBehaviour {
   [SerializeField] private Animator anim;
   [SerializeField] private CatFollowPlayer follow;
   [SerializeField]private bool canMove;
   [SerializeField] private bool followPlayer;
   [SerializeField] private float moveSpeed = 1;
   [SerializeField] private float turnSpeed = 1;

   private bool turnClockwise;
   private bool turnCounter;
   private bool moveForward;

   private Vector2 dir;
   private Quaternion toRot;
   private float rotDiff;
   void Update()
   {
      GetInput();
      SetTurnBoolValues();
      
      SetAnimVariables();
      Move();
      Rotate();
   }

   void FollowInput()
   {
      if (!followPlayer) return;
      if (follow.CloseToPlayer)
      {
         dir = Vector2.zero;
         return;
      }

      dir = follow.FollowPoint - (Vector2)transform.position;
      dir = dir.normalized;
   }

   void GetInput()
   {
      if (!canMove) return;
      if (followPlayer)
      {
         FollowInput();
         return;
      }
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
      transform.position += (Vector3)dir * moveSpeed * Time.deltaTime;
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
