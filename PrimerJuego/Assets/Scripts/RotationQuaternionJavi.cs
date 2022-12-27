using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerR : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumSpeed;
    public float jumpButtonPeriod;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirections = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirections.magnitude) * speed;

        movementDirections.Normalize();

        //Gravity & Jump

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }


        if (Time.time - lastGroundedTime <= jumpButtonPeriod)
        {
            characterController.stepOffset = originalStepOffset;

            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonPeriod)
            {
                ySpeed = jumSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0f;
        }


        //Rotations
        Vector3 velocity = movementDirections * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirections != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirections, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}