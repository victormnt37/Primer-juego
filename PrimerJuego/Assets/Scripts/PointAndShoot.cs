using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    public Vector3 target;
    public GameObject crosshairs;
    public float crosshairsHeight = 1f;
    public Vector3 mousePos;
    public float ZmousePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = ZmousePos;
        Debug.Log(GetComponent<Camera>().ScreenToWorldPoint(mousePos));

        crosshairs.transform.position = new Vector3(GetComponent<Camera>().ScreenToWorldPoint(mousePos).x, crosshairsHeight, GetComponent<Camera>().ScreenToWorldPoint(mousePos).z);
    }
}
