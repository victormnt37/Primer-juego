using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootCooldown = 0.25f;
    float lastShotTime = 0;

    /*public float Speed = 10;
    public float flyTime = 3f;
*/
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Shot();

        }
        
    }

    void Shot(){

        if (Time.time - lastShotTime > shootCooldown){
            GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
            lastShotTime = Time.time;
        }
    }

   /* private void Awake(){
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
        Invoke("DestroyBullet", flyTime);
    }

    void DestroyBullet(){
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Enemy")){
            Debug.Log("Enemigo detectado");
        }
    }*/
}
