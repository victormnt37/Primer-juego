using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    public Vector3 target;
    public float shootingHeight = 0; //this variable helps calculate the height of the bullet on the screen, taking reference the camera height
    public GameObject crosshairs;
    public Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        mousePos = Input.mousePosition;
        Debug.Log(target);
        crosshairs.transform.position = new Vector2(target.x, target.y);
    }
}
