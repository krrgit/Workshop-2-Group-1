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


    private bool AttackActive;

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

    public void StopAll()
    {
        AttackOne.ToggleEmit(false);
        AttackTwo.ToggleEmit(false);
        AttackThree.ToggleEmit(false);
        AttackFour.ToggleEmit(false);
        
        AttackOne.DestroyAllParticles();
        AttackTwo.DestroyAllParticles();
        AttackThree.DestroyAllParticles();
        AttackFour.DestroyAllParticles();
        
        AttackOne.gameObject.SetActive(false);
        AttackTwo.gameObject.SetActive(false);
        AttackThree.gameObject.SetActive(false);
        AttackFour.gameObject.SetActive(false);
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
