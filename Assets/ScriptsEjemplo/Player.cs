using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed = 3;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce = 5;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    private Vector3 spawn = new Vector3(-15, 2, -117);

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        setGravity();

        playerSkills();

        player.Move(movePlayer * playerSpeed * Time.deltaTime);
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
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
        if (hit.collider.name == "Water")
        {
            electrocute();
        }
        // else if (hit.collider.name == "Platform")
        // {
        //     // TO-DO: player gets flat once enters the platform
        //     transform.SetParent(hit.gameObject.transform, false);
        // }
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

    public void electrocute()
    {
        // TO-DO: animations/count deaths
        transform.position = spawn;
    }
}
