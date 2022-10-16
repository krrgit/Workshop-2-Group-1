using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    //the area where the enemy can move
    //check x & y of the map and input in unity
    public Transform moveSpots;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    
    void Start()
    {
        waitTime = startWaitTime;
        //generates random new spots for the enemy to go to
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    
    //moves randomly and waits a certain time
    void Update()
    {
        //moves randomly
        transform.position = Vector2.MoveTowards(transform.position, 
            moveSpots.position, speed * Time.deltaTime);
        //checks distance between movespots 
        if (Vector2.Distance(transform.position, moveSpots.position) < 0.2f)
        {
            //moves when wait time is 0 to new random position
            if (waitTime <= 0)
            {
                moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minX, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                //waitTime decreases 
                waitTime -= Time.deltaTime;
            }
        }
    }
}
