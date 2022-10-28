using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class SpiderAIController : MonoBehaviour
{
    [SerializeField] private SpiderAttackController attack;
    private bool PatternActive = false;
    public int randomNum = 0;
    private float timer = 0;
    void Update()
    {
        randomNum = Random.Range(1, 5);
        Debug.Log(randomNum);
        RandomAttacksGenerator(PatternActive);
        timer -= Time.deltaTime;
    }

    void RandomAttacksGenerator(bool PatternActive)
    {
        if (timer > 0) return;
        for (int i = 0; i < randomNum; i++)
        {
            if (randomNum == 1 && PatternActive == false)
            {
                PatternActive = true;
                attack.DoAttackOne(3);
                timer = 3;
                PatternActive = false;
            }

            else if (randomNum == 2 && PatternActive == false)
            {
                PatternActive = true;
                attack.DoAttackTwo(3);
                timer = 3;
                PatternActive = false;

            }

            else if (randomNum == 3 && PatternActive == false)
            {
                PatternActive = true;
                attack.DoAttackThree(3);
                timer = 3;
                PatternActive = false;
            }
            
            
            else if (randomNum == 4 && PatternActive == false)
            {
                PatternActive = true;
                attack.DoAttackFour(3);
                timer = 3;
                PatternActive = false;

            }
        }
    }
}





//I CANT TAKE IT ANYMORE EATING A BURGER WITH NO HONEY MUSTARD