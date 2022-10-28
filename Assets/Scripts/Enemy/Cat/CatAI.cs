using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum MoveDecision {
    Idle,
    Walk,
    Turn,
    Teleport,
    IdleAttack
}

public class CatAI : MonoBehaviour {
    public bool runAI = false;
    [SerializeField] private CatAnimController anim;
    [SerializeField] private CatAttackController attack;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private Transform[] walkPaths;
    [SerializeField] private Transform idlePoint;
    [SerializeField] private int currPath;
    [SerializeField] private int curWalkPoint = 0;
    [SerializeField] private float staffAtkCooldown = 10;
    private int[] pathTurnDir = { 1, -1 };
    

    private float moveTimer;

    private MoveDecision moveDecision;

    private Vector2 alignment;

    private float tpCooldown;

    private int phase = 1;

    private float staffAttackTimer;

    private bool hailMary;

    private void OnEnable()
    {
        health.enemyDeathDel += Death;
    }

    private void OnDisable()
    {
        health.enemyDeathDel -= Death;
    }


    // Update is called once per frame
    void Update()
    {
        if (!runAI) return;
        
        if (phase == 1)
        {
            WalkAround();
            TryStaffAttack();
        }

        StaffAttackTimerTick();
        MoveTimerTick();
        TeleportCooldownTick();
        
        HailMaryCheck();
    }

    void Death()
    {
        runAI = false;
        anim.StopAllCoroutines();
        attack.StopAllCoroutines();
        anim.StopAll();
        anim.StopAnimator();
        attack.StopAll();
        
    }
    
    // If somehow the cat still goes out of bounds do idle attack
    void HailMaryCheck()
    {
        if (hailMary) return;
        if (transform.localPosition.x > 28 || transform.localPosition.x < -25 || transform.localPosition.y > 12 || transform.localPosition.y < -12)
        {
            anim.StopAllCoroutines();
            anim.StopAll();
            anim.TeleportToPoint(idlePoint);
            moveDecision = MoveDecision.IdleAttack;
            RandomIdleAttack();
            hailMary = true;
            print("Hail Mary");
        }
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
            staffAttackTimer = staffAtkCooldown;
        }
    }

    void WalkAround()
    {
        if (!NextMoveCheck()) return;
        
        hailMary = false;
        
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
            //RandomWalkPoint();
            anim.TeleportToPoint(GetPointFarthestFromPlayer());
            CycleWalkPoint();
            moveDecision = MoveDecision.Teleport;
            tpCooldown = 1;
        }else if (Random.Range(0,10) < 4 && tpCooldown <= 0)
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

    Transform GetPointFarthestFromPlayer()
    {
        Transform point = walkPaths[currPath].GetChild(0);

        float dist;
        float dist1;
        for (int i = 1; i < walkPaths[currPath].childCount; ++i)
        {
            dist = Vector2.Distance(PlayerHealth.Instance.transform.position, point.position);
            dist1 = Vector2.Distance(PlayerHealth.Instance.transform.position, walkPaths[currPath].GetChild(i).position);
            if (dist1 > dist)
            {
                point = walkPaths[currPath].GetChild(i);
            }
        }

        return point;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Collide with wall
        if (col.gameObject.layer == 12)
        {
            print("Cat collid with wall");
            anim.StopAllCoroutines();
            anim.StopAll();
            anim.TeleportToPoint(idlePoint);
            moveDecision = MoveDecision.IdleAttack;
            RandomIdleAttack();
        }
        
    }
    
    
}
