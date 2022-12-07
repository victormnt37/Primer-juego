using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingEntity
{
    public Vector3 centerPt;
    public float radius;

    public GameObject bullet;
    public GameObject shooter;

    public float shootCoolDown = 0.2f;
    public float lastShotTime = 0;

    public Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        if (movement != Vector3.zero)
        {
            MoveTowards(movement.normalized);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gun != null)
            {
                gun.Shoot();
            }
        }

        Vector3 newPos = transform.position + movement;

        // Quaternion towardsTargetRotation = Quaternion.LookRotation(newPos, Vector3.up);
        // transform.position += newPos.normalized * speed * Time.deltaTime;
        // transform.rotation = Quaternion.Lerp(transform.rotation, towardsTargetRotation, rotationSpeed * Time.deltaTime);

        Vector3 offset = newPos - centerPt;
        transform.position = centerPt + Vector3.ClampMagnitude(offset, radius);
    }
}
