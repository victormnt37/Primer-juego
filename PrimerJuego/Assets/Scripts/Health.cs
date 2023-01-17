using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class Health : MonoBehaviour
{

    public float currentHealth = 3;
    public float maxHealth = 3;
    

    public UnityEvent onDamageTaken;
    public UnityEvent onDead;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DamageTaken (float amount){
        currentHealth -= amount;
        onDamageTaken.Invoke();

        if (currentHealth <= 0){
            onDead.Invoke();
        }

   
}
}