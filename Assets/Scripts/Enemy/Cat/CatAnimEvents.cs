using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimEvents : MonoBehaviour {
   [SerializeField] private Vector2 wRPawPos;
   [SerializeField] private Vector2 wLPawPos;
   [SerializeField] private Vector2 tCRPawPos;
   [SerializeField] private Vector2 tCLPawPos;
   [SerializeField] private Vector2 tCCRPawPos;
   [SerializeField] private Vector2 tCCLPawPos;


   public delegate void LandPawDelegate(Vector2 Position);

   public LandPawDelegate landPawDel;

   public void WalkLandRightPawEvent()
   {
      landPawDel(wRPawPos);
   }
   
   public void WalkLandLeftPawEvent()
   {
      landPawDel(wLPawPos);
   }
   
   public void TurnCLandRightPawEvent()
   {
      landPawDel(tCRPawPos);
   }
   
   public void TurnCLandLeftPawEvent()
   {
      landPawDel(tCLPawPos);
   }
   
   public void TurnCCLandRightPawEvent()
   {
      landPawDel(tCCRPawPos);
   }
   
   public void TurnCCLandLeftPawEvent()
   {
      landPawDel(tCCLPawPos);
   }
}
