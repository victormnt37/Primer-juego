using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public float currentHealth = 5;
    

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

    /*void OnTriggerEnter(Collider col){

            currentHealth -= Bala.damage;

            if (currentHealth <= 0){
                enemyDead = true;

                Destroy(gameObject, 1f);
            }
    }*/
}
}