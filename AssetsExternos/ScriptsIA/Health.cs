using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float currentHealth = 5;
    public UnityEvent onDamageTaken;
    public UnityEvent onDead;

    void DamageTaken(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Vida actual: " + currentHealth);
        onDamageTaken.Invoke();

        if (currentHealth <= 0)
        {
            onDead.Invoke();
        }
    }
}
