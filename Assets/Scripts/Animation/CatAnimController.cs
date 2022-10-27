using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimController : MonoBehaviour {
   [SerializeField] private Animator anim;
   [SerializeField] private float moveSpeed = 1;
   [SerializeField] private float turnSpeed = 1;
   [SerializeField] private float pointRange = 5;

   private bool turnClockwise;
   private bool turnCounter;
   private int moveForward;
   private int turnDir; // 1 = clockwise; -1 = counter

   private bool isMoving;
   private bool isTurning;

   public bool TurnActive
   {
      get { return isTurning; }
   }

   public bool MoveActive
   {
      get { return isMoving; }
   }
   
   public float Turn(float duration, int direction)
   {
      if (isTurning) return 0;
      StartCoroutine(ITurn(duration, direction));

      return duration;
   }
   

   public void TurnToFacePoint(Vector2 point)
   {
      if (isTurning) return ;
      
      //int direction = Vector3.Project(point - (Vector2)transform.position, -transform.up).x > 0 ? 1 : -1;
      float right = Vector3.Project(-transform.right,point - (Vector2)transform.position).magnitude;
      float left = Vector3.Project(transform.right,point - (Vector2)transform.position).magnitude;
      int direction = right > left ? -1 : 1;
      
      StartCoroutine(ITurnToPoint(point, direction));
   }
   
   public float Walk(float duration)
   {
      if (isMoving) return 0;
      StartCoroutine(IMove(duration));

      return duration;
   }
   
   public void WalkToPoint(Vector2 point)
   {
      if (isMoving) return;
      StartCoroutine(IMoveToPoint(point));

   }

   public float ComputeTurnTime(float angle)
   {
      return angle / turnSpeed;
   }

   public float ComputeWalkTime(float distance)
   {
      return distance / moveSpeed;
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

   float VectorAlignment(Vector2 point)
   {
      return Vector3.Project(-transform.up, point - (Vector2)transform.position).magnitude;
   }

   float PointDistance(Vector2 point)
   {
      return ((Vector2)transform.position - point).magnitude;
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

   IEnumerator ITurnToPoint(Vector2 point, int direction)
   {
      
      isTurning = true;
      turnDir = direction;
      moveForward = 1;
      SetTurnValues(direction);

      while (VectorAlignment(point) < 0.99f)
      {
         yield return new WaitForEndOfFrame();
      }

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

   IEnumerator IMoveToPoint(Vector2 point)
   {
      moveForward = 1;
      isMoving = true;

      while (PointDistance(point) > pointRange)
      {
         yield return new WaitForEndOfFrame();
      }
      
      isMoving = false;
      moveForward = turnDir == 0 ? 0 : 1;
   }

}
