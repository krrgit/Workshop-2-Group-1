using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpiderAttackController : MonoBehaviour
{
    [SerializeField] private BulletHellSpawner AttackOne;
    [SerializeField] private BulletHellSpawner AttackTwo;
    [SerializeField] private BulletHellSpawner AttackThree;
    [SerializeField] private BulletHellSpawner AttackFour;

    public void DoAttackOne(float duration)
    {
        StartCoroutine(IDoAttackOne(duration));
    }

    public void DoAttackTwo(float duration)
    {
        StartCoroutine(IDoAttackTwo(duration));
    }

    public void DoAttackThree(float duration)
    {
        StartCoroutine(IDoAttackThree(duration));
    }

    public void DoAttackFour(float duration)    
    {
        StartCoroutine(IDoAttackFour(duration));
    }

    private void Start()
    {
        AttackOne.Initialize();
        AttackTwo.Initialize();
        AttackThree.Initialize();
        AttackFour.Initialize();
    }

    private void Update()
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
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            DoAttackFour(3);
        }
    }

    IEnumerator IDoAttackOne(float duration)
    {
        AttackOne.ToggleEmit(true);
        yield return new WaitForSeconds(duration);
        AttackOne.ToggleEmit(false);
    }

    IEnumerator IDoAttackTwo(float duration)
    {
        AttackTwo.ToggleEmit(true);
        yield return new WaitForSeconds(duration);
        AttackTwo.ToggleEmit(false);
    }

    IEnumerator IDoAttackThree(float duration)
    {
        AttackThree.ToggleEmit(true);
        yield return new WaitForSeconds(duration);
        AttackThree.ToggleEmit(false);
    }

    IEnumerator IDoAttackFour(float duration)
    {
        AttackFour.ToggleEmit(true);
        yield return new WaitForSeconds(duration);
        AttackFour.ToggleEmit(false);
    }
}
