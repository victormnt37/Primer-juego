using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float platformSpeed = 10;
    public Rigidbody rb;
    public Transform[] platformPosition;
    private int actualPosition = 0;
    private int nextPosition = 1;

    private bool moveToTheNext = true;
    public float waitTime = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    public void MovePlatform()
    {

        if (moveToTheNext)
        {
            StopCoroutine(WaitForMove(0));
            rb.MovePosition(Vector3.MoveTowards(rb.position, platformPosition[nextPosition].position, platformSpeed * Time.deltaTime));
        }

        if (Vector3.Distance(rb.position, platformPosition[nextPosition].position) <= 0)
        {
            StartCoroutine(WaitForMove(waitTime));
            actualPosition = nextPosition;
            nextPosition++;

            if (nextPosition > platformPosition.Length - 1)
            {
                nextPosition = 0;
            }
        }
    }

    IEnumerator WaitForMove(float time)
    {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }
}
