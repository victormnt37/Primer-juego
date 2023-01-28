using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed = 2;
    public float rotationSpeed = 2;
    // public float gravity = 9.8f;   
    public float fallVelocity;
    public float jumpForce = 3;
    public float runningSpeed = 4f;
    public float acceleration = 5f;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    private Animator anim;
    public float x, y;
    private bool isJumping;
    private bool isGrounded;

    public float jumpButtonPeriod;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    NavMeshAgent agent;

    private Vector3 spawn = new Vector3(0, 0, 0);

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        player = GetComponent<CharacterController>();
        originalStepOffset = player.stepOffset;
        anim = GetComponent<Animator>();
        anim.SetFloat("X", x);
        anim.SetFloat("Y", y);
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        anim.SetFloat("X", horizontalMove);
        anim.SetFloat("Y", verticalMove);

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        float magnitude = Mathf.Clamp01(playerInput.magnitude) * playerSpeed;

        playerInput.Normalize();

        setGravity();

        playerSkills();

        //Rotations
        Vector3 velocity = playerInput * magnitude;
        velocity.y = fallVelocity;

        player.Move(velocity * Time.deltaTime);

        if (playerInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(playerInput, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void setGravity()
    {
        fallVelocity += Physics.gravity.y * Time.deltaTime;

        if (player.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        // slideDown();
    }

    public void playerSkills()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonPeriod)
        {
            player.stepOffset = originalStepOffset;

            fallVelocity = -0.5f;
            anim.SetBool("IsGrounded", true);
            isGrounded = true;
            anim.SetBool("IsJumping", false);
            isJumping = false;
            anim.SetBool("IsFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonPeriod)
            {
                fallVelocity = jumpForce;
                anim.SetBool("IsJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            player.stepOffset = 0f;
            anim.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && fallVelocity < 0) || fallVelocity < -2)
            {
                anim.SetBool("IsFalling", true);
            }
        }

        //Run
        if (Input.GetKey("left shift") && (horizontalMove != 0 || verticalMove != 0))
        {
            if (playerSpeed <= runningSpeed)
            {
                playerSpeed += acceleration * Time.deltaTime;
            }
        }
        else if (playerSpeed >= 2.02)
        {
            {
                playerSpeed -= acceleration * Time.deltaTime;
            }
        }

        //Shoot
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.LookAt(target, Vector3.up);
            }
        }
    }

    // public void slideDown()
    // {
    //     isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

    //     if (isOnSlope)
    //     {
    //         movePlayer.x += ((1f - hitNormal.y) * hitNormal.x * slideVelocity);
    //         movePlayer.z += ((1f - hitNormal.y) * hitNormal.z * slideVelocity);

    //         movePlayer.y = slopeForceDown;
    //     }
    // }
}
