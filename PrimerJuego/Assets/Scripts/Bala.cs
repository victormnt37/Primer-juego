using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private GameObject player;
    public GameObject boom;
    public float Speed = 10;
    public float flyTime = 10f;

    Rigidbody rb;

    //Salud
    public float damage=2f;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player");
       // boom=GameObject.Find("Boom");

    }
   private void Awake(){
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
        Invoke("DestroyBullet", flyTime);
    }
    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(player.transform.position,transform.position)>100){
               Debug.Log("desapareguem per distancia") ;
               if (gameObject != null){
                Destroy(gameObject);

               }
        } 
    }
   

    void DestroyBullet(){
        Destroy(gameObject);
    }
     void OnTriggerEnter(Collider b){
            if(b.gameObject.tag=="Enemigo"){
            Destroy(b.gameObject);
            GameObject be = Instantiate(boom,transform.position,transform.rotation);
            Destroy(be,2);
        }
            if (b.CompareTag("Enemigo")){
                b.SendMessage("DamageTaken", damage);
                DestroyBullet();
            }
            //Destroy(gameObject);

    }

}
