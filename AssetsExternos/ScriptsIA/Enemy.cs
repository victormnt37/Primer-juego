using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingEntity
{
    public Vector3 randomPosition;
    public Vector3 newPosition;

    float wanderRadius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        RecalculateTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = randomPosition - transform.position;

        if (newPosition.magnitude < 0.25f)
        {
            RecalculateTargetPosition();
        }

        MoveTowards(newPosition.normalized);

        Debug.DrawLine(transform.position, randomPosition, Color.yellow);
    }

    void RecalculateTargetPosition()
    {
        randomPosition = transform.position + Random.insideUnitSphere * wanderRadius;
        randomPosition.y = 0;

    }

    public void onDeadHandler()
    {
        Destroy(gameObject);
    }
}
