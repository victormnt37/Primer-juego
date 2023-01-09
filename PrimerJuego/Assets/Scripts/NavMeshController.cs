using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshController : MonoBehaviour
{

    public Transform enemy;


    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = enemy.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
