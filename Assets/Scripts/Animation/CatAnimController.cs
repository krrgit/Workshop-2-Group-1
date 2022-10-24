using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimController : MonoBehaviour {
   [SerializeField] private Animator anim;
   [SerializeField] private float moveSpeed = 1;
   [SerializeField] private float turnSpeed = 1;

   private bool turnClockwise;
   private bool turnCounter;
   private int moveForward;
   private int turnDir;
   
   void Update()
   {
      if (Input.GetKey(KeyCode.A))
      {
         turnClockwise = Input.GetKey(KeyCode.A);
         turnCounter = false;
      }
      else
      {
         turnCounter = Input.GetKey(KeyCode.D);
         turnClockwise = false;
      }

      DemoInputs();
      
      SetAnimVariables();
      Move();
      Rotate();
   }


   void DemoInputs()
   {
      moveForward = Input.GetKey(KeyCode.W) ? 1 : 0;
      turnDir = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
   }

   void SetAnimVariables()
   {
      anim.SetBool("turnClockwise", turnClockwise);
      anim.SetBool("turnCounter", turnCounter);
      anim.SetBool("moveForward", Input.GetKey(KeyCode.W));
   }

   void Move()
   {
      transform.position += transform.up * -moveSpeed * moveForward * Time.deltaTime;
   }

   void Rotate()
   {
      transform.Rotate(0,0,turnDir * turnSpeed * Time.deltaTime);
   }

   public void Turn(float duration, int direction)
   {
      
   }

   public void Walk(float duration)
   {
      
   }

}
