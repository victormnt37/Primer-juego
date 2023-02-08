using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{

    public float movementSpeed = 1f;
    Vector3 targetPosition;
    Vector3 towardsTarget;
    float wanderRadius = 5f;
    public Transform objective;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        RecalculateTargetPosition();
        agent=GetComponent<UnityEngine.AI.NavMeshAgent>();

        objective = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = objective.position;
        
        towardsTarget = targetPosition - transform.position;

        if (towardsTarget.magnitude < 0.25f)
        {

            RecalculateTargetPosition();

        }

        transform.position += towardsTarget.normalized * movementSpeed * Time.deltaTime;
        Debug.DrawLine(transform.position, targetPosition, Color.green);

    }

    void RecalculateTargetPosition()
    {

        targetPosition = transform.position + Random.insideUnitSphere * wanderRadius;
        targetPosition.y = 0;

    }
    public void onDeadHandler()
    {
        Destroy(gameObject);
    }
    
    
}

