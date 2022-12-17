using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed = 2;
    public float rotationSpeed = 2;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce = 3;
    public float runningSpeed = 4f;
    public float acceleration = 0.025f;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    // private Animator anim;

    private Vector3 spawn = new Vector3(0, 0, 0);

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        //Controlling inputs
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        float magnitude = playerInput.magnitude;
        // playerInput = Vector3.ClampMagnitude(playerInput, 1);

        magnitude = Mathf.Clamp01(magnitude);
        playerInput.Normalize();

        //Camera
        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        //Movement
        setGravity();

        playerSkills();

        // player.Move(movePlayer * playerSpeed * Time.deltaTime);
        transform.Translate(playerInput * magnitude * playerSpeed * Time.deltaTime, Space.World);

        if (playerInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(playerInput, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void setGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
        }
        movePlayer.y = fallVelocity;

        slideDown();
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void playerSkills()
    {
        //Jump
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }

        //Run
        if (Input.GetKey("left shift") && playerSpeed >= 2)
        {
            if (playerSpeed <= runningSpeed)
            {
                playerSpeed += acceleration;
            }
        }
        else if (playerSpeed >= 2.025)
        {
            {
                playerSpeed -= acceleration;
            }
        }
    }

    public void slideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if (isOnSlope)
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x * slideVelocity);
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z * slideVelocity);

            movePlayer.y = slopeForceDown;
        }
    }
}
