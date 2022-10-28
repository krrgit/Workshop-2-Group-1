using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDecision {
    Idle,
    Walk,
    Turn,
    Teleport,
    IdleAttack
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

    private float staffAttackTimer;

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
            TryStaffAttack();
        } else if (phase == 2)
        {
            
        }

        StaffAttackTimerTick();
        MoveTimerTick();
        TeleportCooldownTick();
    }

    void StaffAttackTimerTick()
    {
        if (staffAttackTimer <= 0) return;
        staffAttackTimer -= Time.deltaTime;
    }

    void MoveTimerTick()
    {
        if (moveTimer <= 0) return;
        moveTimer -= Time.deltaTime;
    }

    void TeleportCooldownTick()
    {
        if (tpCooldown <= 0) return;

        tpCooldown -= Time.deltaTime;
    }

    void TryStaffAttack()
    {
        if (attack.IsAttacking) return;

        if ((moveDecision == MoveDecision.Walk && anim.MoveActive) ||
            (moveDecision == MoveDecision.Turn && anim.TurnActive))
        {
            attack.DoStaffAttack();
            staffAttackTimer = 5;
        }
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
        } else if (TeleportCheck()) {
            // Teleport
            RandomPath();
            RandomWalkPoint();
            anim.TeleportToPoint(walkPaths[currPath].GetChild(curWalkPoint));
            CycleWalkPoint();
            moveDecision = MoveDecision.Teleport;
            tpCooldown = 1;
        }else if (Random.Range(0,10) < 6 && tpCooldown <= 0)
        {
            // Idle Attack
            anim.TeleportToPoint(idlePoint);
            moveDecision = MoveDecision.IdleAttack;
            RandomIdleAttack();
        }
        else
        {
            // Walk to Point
            anim.WalkToPoint(walkPaths[currPath].GetChild(curWalkPoint).position);
            moveDecision = MoveDecision.Walk;
        }
    }

    void RandomIdleAttack()
    {
        int r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                attack.DoChargeAttack(2f/3f);
                moveTimer = attack.StaffAtkTotalDur;
                break;
            case 1:
                attack.DoButterflyAttack(2f/3f);
                moveTimer = attack.ButterFlyAtkTotalDur;

                break;
            default:
                break;
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

        if (moveDecision == MoveDecision.IdleAttack && moveTimer > 0)
        {
            return false;
        }
        
         return true;
    }
    
    
    
}
