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

   private bool isMoving;
   private bool isTurning;
   
   public void Turn(float duration, int direction)
   {
      if (isTurning) return;
      StartCoroutine(ITurn(duration, direction));
   }

   public void TurnToDir(Vector2 dir)
   {
      if (isTurning) return;

      var duration = ComputeTurnTime(Vector2.Angle(dir, -transform.up));
      int direction = Vector3.Project(dir, -transform.up).x > 0 ? 1 : -1;
      
      StartCoroutine(ITurn(duration,direction));
   }
   
   public void Walk(float duration)
   {
      if (isMoving) return;
      StartCoroutine(IMove(duration));
   }

   public float ComputeTurnTime(float angle)
   {
      return angle / turnSpeed;
   }

   void Update()
   {
      //DemoInputs();
      
      SetAnimVariables();
      Move();
      Rotate();
   }
   
   void DemoInputs()
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
      
      moveForward = Input.GetKey(KeyCode.W) ? 1 : 0;
      turnDir = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
   }

   void SetAnimVariables()
   {
      anim.SetBool("turnClockwise", turnClockwise);
      anim.SetBool("turnCounter", turnCounter);
      anim.SetBool("moveForward", moveForward == 1? true : false);
   }

   void Move()
   {
      transform.position += transform.up * -moveSpeed * moveForward * Time.deltaTime;
   }

   void Rotate()
   {
      transform.Rotate(0,0,turnDir * turnSpeed * Time.deltaTime);
   }

   void SetTurnValues(int direction)
   {
      if (direction == 1)
      {
         turnClockwise = true;
         turnCounter = false;
      } else if (direction == -1)
      {
         turnCounter = true;
         turnClockwise = false;
      }
      else
      {
         turnCounter = false;
         turnClockwise = false;
      }
   }
   
   IEnumerator ITurn(float duration, int direction)
   {
      isTurning = true;
      turnDir = direction;
      moveForward = 1;
      SetTurnValues(direction);

      yield return new WaitForSeconds(duration);
      
      turnDir = 0;
      moveForward = isMoving ? moveForward : 0;
      SetTurnValues(0);
      isTurning = false;
   }
   
   IEnumerator IMove(float duration)
   {
      moveForward = 1;
      isMoving = true;
      yield return new WaitForSeconds(duration);
      isMoving = false;
      moveForward = turnDir == 0 ? 0 : 1;
   }

}
