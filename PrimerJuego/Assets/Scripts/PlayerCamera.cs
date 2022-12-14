using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 distance;
    private Transform target;
    [Range(0, 1)] public float lerpValue;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player1").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //El LateUpdate se suele gastar para la cámara
        transform.position = Vector3.Lerp(target.position, target.position + distance, lerpValue);

        transform.LookAt(target);
    }
}
