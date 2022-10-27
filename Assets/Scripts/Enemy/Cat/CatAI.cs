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

    [SerializeField] private Transform[] walkPaths;
    [SerializeField] private Transform idlePoint;
    [SerializeField] private int currPath;
    [SerializeField] private int curWalkPoint = 0;
    private int[] pathTurnDir = { 1, -1 };
    

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
        if (phase == 1)
        {
            WalkAround();
        } else if (phase == 2)
        {
            
        }
        

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
        
        alignment = Vector3.Project(-transform.up, walkPaths[currPath].GetChild(curWalkPoint).position - transform.position);
        if (alignment.magnitude < 0.99f)
        {
            // Turn
            anim.TurnToFacePoint(walkPaths[currPath].GetChild(curWalkPoint).position,pathTurnDir[currPath]);
            moveDecision = MoveDecision.Turn;
            print("Turn");
        } else if (TeleportCheck())
        {
            // Teleport
            RandomPath();
            RandomWalkPoint();
            anim.TeleportToPoint(walkPaths[currPath].GetChild(curWalkPoint));
            CycleWalkPoint();
            moveDecision = MoveDecision.Teleport;
            tpCooldown = 2;
            print("Teleport");
        }else if (Random.Range(0,4) == 2 && tpCooldown <= 0)
        {
            // Idle Test
            //anim.TeleportToPoint(idlePoint);
            //moveDecision = MoveDecision.Idle;
            //print("Teleport to Center");
        }
        else
        {
            // Walk to Point
            anim.WalkToPoint(walkPaths[currPath].GetChild(curWalkPoint).position);
            moveDecision = MoveDecision.Walk;
            print("Walk");
        }
    }

    bool TeleportCheck()
    {
        return (!attack.IsAttacking && Random.Range(0, 2) == 1 && tpCooldown <= 0);
    }

    bool StationaryAttackCheck()
    {
        return (moveDecision == MoveDecision.Walk && tpCooldown <= 0 && Random.Range(0,3) == 0);
    }

    void RandomPath()
    {
        currPath = Random.Range(0, walkPaths.Length);
    }

    void RandomWalkPoint()
    {
        int r = Random.Range(0, walkPaths[currPath].childCount);
        while (r != GetNextWalkPoint())
        {
            r = Random.Range(0, walkPaths[currPath].childCount);
        }

        curWalkPoint = r;
    }

    int GetNextWalkPoint()
    {
        return curWalkPoint+1 >= walkPaths[currPath].childCount ? 0 : curWalkPoint;
    }

    void CycleWalkPoint()
    {
        ++curWalkPoint;
        curWalkPoint = curWalkPoint >= walkPaths[currPath].childCount ? 0 : curWalkPoint;
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
