using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // public Rigidbody bulletRB;
    public float Speed = 10;
    public float flyTime = 3f;
    public float damage = 2f;
    Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
        Invoke("DestroyBullet", flyTime);
    }

    void Update()
    {

    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.SendMessage("DamageTaken", damage);
            // GameObject b = Instantiate(boom, transform.position, transform.rotation);
            // Destroy(b, 3);
            Destroy(gameObject);
        }
    }
}
