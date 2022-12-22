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
        target = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = target.position + new Vector3(0, height, lenght);
    }
}
