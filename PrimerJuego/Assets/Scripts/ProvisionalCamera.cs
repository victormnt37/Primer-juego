using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvisionalCamera : MonoBehaviour
{
    public float height;
    public float lenght;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player1").transform;
    }

    private void LateUpdate()
    {
        transform.position = target.position + new Vector3(lenght, height, 0);
    }
}
