using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float shootCooldown = 0.25f;
    float lastShotTime = 0;

    public void Shoot()
    {
        if (Time.time - lastShotTime > shootCooldown)
        {
            GameObject.Instantiate(bullet, transform.position, transform.rotation);
            lastShotTime = Time.time;
        }
    }
}
