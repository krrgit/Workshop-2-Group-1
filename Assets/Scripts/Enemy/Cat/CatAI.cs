using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDecision {
    Idle,
    Walk,
    Turn,
}

public class CatAI : MonoBehaviour {
    [SerializeField] private CatAnimController anim;
    [SerializeField] private CatAttackController attack;

    [SerializeField] private Transform[] walkPath;
    [SerializeField] private int curWalkPoint = 0;

    private float moveTimer;

    private MoveDecision moveDecision;

    private Vector2 alignment;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WalkAround();

        MoveTimerTick();
    }

    void MoveTimerTick()
    {
        moveTimer -= Time.deltaTime;
    }

    void WalkAround()
    {
        if (!NextMoveCheck()) return;

        if (moveDecision == MoveDecision.Walk)
        {
            CycleWalkPoint();
        }
        
        alignment = Vector3.Project(-transform.up, walkPath[curWalkPoint].position - transform.position);
        if (alignment.magnitude < 0.99f)
        {
            anim.TurnToFacePoint(walkPath[curWalkPoint].position);
            moveDecision = MoveDecision.Turn;
            print("Turn");
        }
        else
        {
            anim.WalkToPoint(walkPath[curWalkPoint].position);
            moveDecision = MoveDecision.Walk;
            print("Walk");
        }
    }

    void CycleWalkPoint()
    {
        ++curWalkPoint;
        curWalkPoint = curWalkPoint >= walkPath.Length ? 0 : curWalkPoint;
    }

    bool NextMoveCheck()
    {
        if (moveDecision == MoveDecision.Turn && anim.TurnActive)
        {
            return false;
        }

        if (moveDecision == MoveDecision.Walk && anim.MoveActive)
        {
            return false;
        }
        
         return true;
    }
    
    
    
}
