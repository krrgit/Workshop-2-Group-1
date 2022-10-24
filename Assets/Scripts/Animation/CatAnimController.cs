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
   
      
   public void Turn(float duration, int direction)
   {
      StartCoroutine(ITurn(duration, direction));
   }
   
   public void Walk(float duration)
   {
      StartCoroutine(IMove(duration));
   }

   void Update()
   {
      DemoInputs();
      
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
      turnDir = direction;
      SetTurnValues(direction);

      yield return new WaitForSeconds(duration);
      
      turnDir = 0;
      SetTurnValues(0);
   }
   
   IEnumerator IMove(float duration)
   {
      moveForward = 1;
      yield return new WaitForSeconds(duration);
      moveForward = 0;
   }

}
