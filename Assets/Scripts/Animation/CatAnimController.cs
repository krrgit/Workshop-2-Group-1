using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CatAnimController : MonoBehaviour {
   [SerializeField] private Animator anim;
   [SerializeField] private SpriteRenderer sr;
   [SerializeField] private Collider2D coll;
   [SerializeField] private float moveSpeed = 1;
   [SerializeField] private float turnSpeed = 1;
   [SerializeField] private float pointRange = 5;
   [SerializeField] private float teleportSpeed = 3;

   private bool turnClockwise;
   private bool turnCounter;
   private int moveForward;
   private int turnDir; // 1 = clockwise; -1 = counter

   private bool isMoving;
   private bool isTurning;
   private bool isTeleporting;
   private bool isIdling;
   private Vector2 idlePointDist;

   public bool TeleportActive
   {
      get { return isTeleporting; }
   }

   public bool TurnActive
   {
      get { return isTurning; }
   }

   public bool MoveActive
   {
      get { return isMoving; }
   }

   public bool IdlingActive
   {
      get { return isIdling; }
   }
   
   public void TurnToFacePoint(Vector2 point, int dir)
   {
      if (isTurning) return ;
      
      //int direction = Vector3.Project(point - (Vector2)transform.position, -transform.up).x > 0 ? 1 : -1;
      //float right = Vector3.Project(-transform.right,point - (Vector2)transform.position).magnitude;
      //float left = Vector3.Project(transform.right,point - (Vector2)transform.position).magnitude;
      //int direction = right > left ? -1 : 1;
      
      StartCoroutine(ITurnToPoint(point, dir));
   }
   
   public void WalkToPoint(Vector2 point)
   {
      if (isMoving) return;
      StartCoroutine(IMoveToPoint(point));

   }

   public void TeleportToPoint(Transform point)
   {
      StartCoroutine(ITeleportToPoint(point));
   }

   float ComputeTurnTime(float angle)
   {
      return angle / turnSpeed;
   }

   float ComputeWalkTime(float distance)
   {
      return distance / moveSpeed;
   }

   public void StopAll()
   {
      isMoving = false;
      moveForward = 0;
      turnDir = 0;
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

   IEnumerator ITeleportToPoint(Transform point)
   {
      coll.enabled = false;
      isTeleporting = true;
      //Fade Out
      Color color = sr.color;
      while (color.a > 0)
      {
         color.a -= Time.deltaTime * teleportSpeed;
         sr.color = color;
         yield return new WaitForEndOfFrame();
      }

      color.a = 0;
      sr.color = color;
      transform.position = point.position;
      transform.rotation = point.rotation;


      // Fade In
      while (color.a < 1)
      {
         color.a += Time.deltaTime * teleportSpeed;
         sr.color = color;
         yield return new WaitForEndOfFrame();
      }

      color.a = 1;
      sr.color = color;
      coll.enabled = true;
      isTeleporting = false;
   }

   IEnumerator Idling(float duration)
   {
      isIdling = true;
      yield return new WaitForSeconds(duration);
      isIdling = false;

   }
      
   

}
