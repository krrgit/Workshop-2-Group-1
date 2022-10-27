using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDecision {
    Idle,
    Walk,
    Turn,
    Teleport
}

public class CatAI : MonoBehaviour {
    [SerializeField] private CatAnimController anim;
    [SerializeField] private CatAttackController attack;

    [SerializeField] private Transform[] walkPath;
    [SerializeField] private Transform idlePoint;
    [SerializeField] private int curWalkPoint = 0;

    private float moveTimer;

    private MoveDecision moveDecision;

    private Vector2 alignment;

    private float tpCooldown;

    private int phase = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WalkAround();

        TeleportCooldownTick();
    }

    void TeleportCooldownTick()
    {
        if (tpCooldown <= 0) return;

        tpCooldown -= Time.deltaTime;
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
        } else if (Random.Range(0,2) == 1 && tpCooldown <= 0)
        {
            RandomWalkPoint();
            anim.TeleportToPoint(walkPath[curWalkPoint]);
            CycleWalkPoint();
            moveDecision = MoveDecision.Teleport;
            tpCooldown = 4;
            print("Teleport");
        }else if (Random.Range(0,4) == 2 && tpCooldown <= 0)
        {
            
            //anim.TeleportToPoint(idlePoint);
            //moveDecision = MoveDecision.Idle;
            //print("Teleport to Center");
        }
        else
        {
            anim.WalkToPoint(walkPath[curWalkPoint].position);
            moveDecision = MoveDecision.Walk;
            print("Walk");
        }
    }

    void RandomWalkPoint()
    {
        int r = Random.Range(0, 4);
        while (r != curWalkPoint)
        {
            r = Random.Range(0, 4);
        }

        curWalkPoint = r;
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

        if (moveDecision == MoveDecision.Teleport && anim.TeleportActive)
        {
            return false;
        }
        
         return true;
    }
    
    
    
}
