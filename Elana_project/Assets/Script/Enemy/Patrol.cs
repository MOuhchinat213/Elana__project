using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float speed=0.5f;
    [SerializeField] private float waitTime;
    [SerializeField] private float startWaitTime=3f;
    public List<Transform> patrolPoints;
    private int randomPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed=0.5f;
        randomPoint = 1;
        startWaitTime=3f;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector2.MoveTowards(transform.position, patrolPoints[randomPoint].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, patrolPoints[randomPoint].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomPoint = randomPoint==1 ? 0 :1;
                waitTime -= startWaitTime;
            }
            else
            {
                waitTime-= Time.deltaTime;
            }
        }
    }
}
