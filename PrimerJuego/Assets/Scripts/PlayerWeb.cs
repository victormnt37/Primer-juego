using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;


public class Player : MonoBehaviour
{
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

    // public Camera mainCamera;
    // private Vector3 camForward;
    // private Vector3 camRight;
    // private Vector3 movePlayer;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    private Animator anim;
    public float x, y;

    public float jumpButtonPeriod;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    public GameObject crosshairs;

    private Vector3 spawn = new Vector3(0, 0, 0);

    //Network
    Vector3 despTemp;
    NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();

    void Start()
    {
        player = GetComponent<CharacterController>();
        originalStepOffset = player.stepOffset;
        anim = GetComponent<Animator>();
        anim.SetFloat("X", x);
        anim.SetFloat("Y", y);
    }

    void Update()
    {
        anim.SetFloat("X", horizontalMove);
        anim.SetFloat("Y", verticalMove);

        if (IsOwner)
        {
            Play();
        }
    }

    public void Play()
    {
        despTemp = new Vector3(0, 0, 0);

        despTemp = PlayerMoveKeyboardAxix();

        if (IsServer)
        {
            float magnitude = Mathf.Clamp01(despTemp.magnitude) * playerSpeed;

            despTemp.Normalize();

            setGravity();

            playerSkills();

            //Rotations
            Vector3 velocity = despTemp * magnitude;
            velocity.y = fallVelocity;

            player.Move(velocity * Time.deltaTime); //This does not work. Cannot move character controller "player" since is not a network variable

            if (despTemp != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(despTemp, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            Rotation.Value = transform.rotation;
            Position.Value = transform.position;

            SubmitChangeRequestClientRpc();
        }
        else
        {
            SubmitChangeRequestServerRpc(despTemp);
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

            if (Time.time - jumpButtonPressedTime <= jumpButtonPeriod)
            {
                fallVelocity = jumpForce;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            player.stepOffset = 0f;
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
            // Quaternion target = Quaternion.LookRotation(crosshairs.transform.position, Vector3.up);
            // Debug.Log(target);
            // Vector3 shootingDirection = Vector3.RotateTowards(transform.forward, crosshairs.transform.position, rotationSpeed * 5 * Time.deltaTime, 0.0f);
            // transform.rotation = Quaternion.LookRotation(shootingDirection);
            transform.LookAt(crosshairs.transform.position, Vector3.up);
        }
    }

    public Vector3 PlayerMoveKeyboardAxix()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        Vector3 mov = new Vector3(0, 0, 0);
        mov = new Vector3(horizontalMove, 0, verticalMove);

        return mov;
    }

    [ServerRpc]
    void SubmitChangeRequestServerRpc(Vector2 desp, ServerRpcParams rpcParams = default)
    {
        Desplace(desp);
    }

    [ClientRpc]
    void SubmitChangeRequestClientRpc(ClientRpcParams clientRpcParams = default)
    {
        transform.position = Position.Value;
        transform.rotation = Rotation.Value;
    }
}
