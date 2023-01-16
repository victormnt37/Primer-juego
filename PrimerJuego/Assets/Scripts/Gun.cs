using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bulletPrefab;
    public float shootCooldown = 0.25f;
    float lastShotTime = 0;
    // public Gun gun;


    public void Shoot()
    {
        if (Time.time - lastShotTime > shootCooldown)
        {
            GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
            lastShotTime = Time.time;
        }
        // if (Input.GetKeyDown (KeyCode.Mouse0)){
        //     gun.Shoot ();
        //     }
    }
}