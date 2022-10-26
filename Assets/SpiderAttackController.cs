using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackController : MonoBehaviour
{
    [SerializeField] private BulletHellSpawner AttackOne;
    [SerializeField] private BulletHellSpawner AttackTwo;
    [SerializeField] private BulletHellSpawner AttackThree;
    [SerializeField] private BulletHellSpawner AttackFour;

    private bool AttackThreeActive = true;

    public void DoAttackOne(float duration)
    {
        StartCoroutine(IAttackOne(duration));
    }

    public void DoAttackTwo(float duration)
    {
        StartCoroutine(IAttackTwo(duration));
    }

    public void DoAttackThree(float duration)
    {
        StartCoroutine(IAttackThree(duration));
    }

    public void DoAttackFour(float duration)
    {
        StartCoroutine(IAttackFour(duration));
    }

    void Start()
    {
        AttackOne.Initialize();
        AttackTwo.Initialize();
        AttackThree.Initialize();
        AttackFour.Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            DoAttackOne(3);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            DoAttackTwo(3);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            DoAttackThree(3);
            while (AttackThreeActive == false)
            {
                DoAttackFour(3);
            }
        }

      
    }
    
    IEnumerator IAttackOne(float duration)
    {
        AttackOne.ToggleEmit(true);
        yield return new WaitForSeconds(duration);
        AttackOne.ToggleEmit(false);
    }

    IEnumerator IAttackTwo(float duration)
    {
        AttackTwo.ToggleEmit(true);
        yield return new WaitForSeconds(duration);
        AttackTwo.ToggleEmit(false);
    }

    IEnumerator IAttackThree(float duration)
    {
        AttackThree.ToggleEmit(true);
        
        yield return new WaitForSeconds(duration);
        AttackThree.ToggleEmit(false);
        AttackThreeActive = false;
    }

    IEnumerator IAttackFour(float duration)
    {
        
        AttackFour.ToggleEmit(true);
        yield return new WaitForSeconds(duration);
        AttackFour.ToggleEmit(false);
        
        
    }
}
