using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegManager : MonoBehaviour {
    public SpiderLegIK[] rightLegs;
    public SpiderLegIK[] leftLegs;

    public int rPair;
    public int lPair = 1;

    private bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (rightLegs[rPair].IdealDistanceCheck())
        {
            if (!isMoving)
            {
                StartCoroutine(MoveLegs());
            }
        }
    }

    IEnumerator MoveLegs()
    {
        isMoving = true;
        while (LegAtIdealCheck()) 
        {
            rightLegs[rPair].MoveIncrement();
            rightLegs[rPair+2].MoveIncrement();
            leftLegs[lPair].MoveIncrement();
            leftLegs[lPair+2].MoveIncrement();
            yield return new WaitForEndOfFrame();
        }

        rPair = rPair == 0 ? 1 : 0;
        lPair = lPair == 0 ? 1 : 0;
        
        isMoving = false;
    }

    bool LegAtIdealCheck()
    {
        return rightLegs[rPair].IdealDistCheck()|| rightLegs[rPair + 2].IdealDistCheck()||
               leftLegs[lPair].IdealDistCheck() || leftLegs[lPair + 2].IdealDistCheck();
    }
}
