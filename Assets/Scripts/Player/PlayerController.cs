using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float rotationSpeedInRadians = 10;
    public float runSpeed = 8f;
    public float walkSpeed = 5f;

    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 playerMovement = Vector3.zero;
    private bool groundedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called for each frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        SetMovementAnimation();
    }

    /// <summary>
    /// To be executed in update. Moves the character according to current input.
    /// Handles gravity and jumping.
    /// </summary>
    void MovePlayer()
    {
        // Remove all negative Y-axis velocity if already grounded
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Read input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Calculate player movement speed and move accordingly
        Vector3 movementDirection = new Vector3(moveHorizontal, 0, moveVertical);

        if (isRunning)
        {
            playerMovement = movementDirection * runSpeed;
        }
        else
        {
            playerMovement = movementDirection * walkSpeed;
        }
        characterController.Move(playerMovement * Time.deltaTime);

        // Calculate and add gravity
        Vector3 gravitySpeed;

        if (groundedPlayer)
        {
            gravitySpeed = new Vector3(0, 0, 0);
        }
        else
        {
            gravitySpeed = new Vector3(0, Physics.gravity.y, 0);
        }
        characterController.Move(gravitySpeed * Time.deltaTime);
    }

    /// <summary>
    /// Tries to rotats the player according to the current movement direction.
    /// Rotation is limited by the player's maximum rotation speed.
    /// </summary>
    void RotatePlayer()
    {
        if (Vector3.zero == playerMovement)
        {
            // If we're not moving anywhere, don't rotate.
            return;
        }

        Vector3 targetDirection = playerMovement.normalized;
        Debug.DrawRay(transform.position, targetDirection, Color.red);

        float rotationStep = rotationSpeedInRadians * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        Debug.DrawRay(transform.position, targetDirection, Color.green);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    /// <summary>
    /// Gets the maximum magnitude of the movement directions to decide if
    /// the character should be running, walking, or standing idle.
    /// </summary>
    void SetMovementAnimation()
    {
        float maxMovement = Mathf.Max(Mathf.Abs(playerMovement.x),
                                      Mathf.Abs(playerMovement.z));
        animator.SetFloat("MovementSpeed", maxMovement);
    }
}