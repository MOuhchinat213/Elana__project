using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float waitTime;
    [SerializeField] private float startWaitTime = 3f;
    public List<Transform> patrolPoints;
    private int randomPoint;
    private Transform player;
    private float detectionRange = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 0.5f;
        randomPoint = 1;
        startWaitTime = 3f;
        player = GameObject.FindWithTag("Player").transform;


    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            Follow_player();
        }
        else
        {
            Being_On_Patrol();
        }

    }

    void Follow_player()
    {
        speed = 1f;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            Debug.Log("Im following the player");
            
        
    }

    void Being_On_Patrol()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            return;
        }

        speed = 0.5f;
        transform.position =
            Vector2.MoveTowards(transform.position, patrolPoints[randomPoint].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, patrolPoints[randomPoint].position) < 0.2f)
        {
            Debug.Log("Im Patroling");
            if (waitTime <= 0)
            {
                randomPoint = randomPoint == 1 ? 0 : 1;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
