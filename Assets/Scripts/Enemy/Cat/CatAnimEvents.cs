using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimEvents : MonoBehaviour {
   [SerializeField] private Transform leftPawPoint;
   [SerializeField] private Transform rightPawPoint;

   public delegate void LandPawDelegate(Vector2 Position);

   public LandPawDelegate landPawDel;

   public void WalkLandRightPawEvent()
   {
      landPawDel(rightPawPoint.position);
   }
   
   public void WalkLandLeftPawEvent()
   {
      landPawDel(leftPawPoint.position);
   }
}
