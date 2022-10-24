using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimController : MonoBehaviour {
   [SerializeField] private Animator anim;
   private bool turnClockwise;
   private bool turnCounter;

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
      
      anim.SetBool("turnClockwise", turnClockwise);
      anim.SetBool("turnCounter", turnCounter);
   }

}
