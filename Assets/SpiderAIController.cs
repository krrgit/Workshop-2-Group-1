using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAIController : MonoBehaviour
{
    [SerializeField] private SpiderAttackController attack;
    

    public float randomNum = 0f;
    

    void Update()
    {
        randomNum = Random.Range(1, 5);
        Debug.Log(randomNum);
        RandomAttacksGenerator();
    }

    void RandomAttacksGenerator()
    {
        for (int i = 0; i < randomNum; i++)
        {
            if (randomNum == 1.0f)
            {    
                attack.DoAttackOne(3);
            }

            else if (randomNum == 2.0f)
            {
                attack.DoAttackTwo(3);
            }

            else if (randomNum == 3.0f)
            {
                attack.DoAttackThree(3);
            }
            
            else if (randomNum == 4.0f)
            {
                attack.DoAttackFour(3);
            }
        }
    }
}
