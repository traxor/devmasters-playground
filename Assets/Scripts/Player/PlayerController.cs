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

    //private Vector3 playerVelocity = Vector3.zero;
    private Vector3 movementInput = Vector3.zero;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    /// <summary>
    /// Update is called for each frame.
    /// </summary>
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
        // TODO: Code below is a preparation for jumping mechanics
        // Remove all negative Y-axis velocity if already grounded
        //if (characterController.isGrounded && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        // Read input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Calculate player movement speed and move accordingly
        Vector3 movementDirection = new Vector3(moveHorizontal, 0, moveVertical);

        if (isRunning)
        {
            movementInput = movementDirection * runSpeed;
        }
        else
        {
            movementInput = movementDirection * walkSpeed;
        }
        characterController.Move(movementInput * Time.deltaTime);

        // Calculate and add gravity
        Vector3 gravitySpeed;

        if (characterController.isGrounded)
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
    /// Tries to rotate the player towards the current movement direction.
    /// Rotation is limited by the player's maximum rotation speed.
    /// </summary>
    void RotatePlayer()
    {
        if (Vector3.zero == movementInput)
        {
            // If we're not moving anywhere, don't rotate.
            return;
        }

        Vector3 targetDirection = movementInput.normalized;
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
        float maxMovement = Mathf.Max(Mathf.Abs(movementInput.x),
                                      Mathf.Abs(movementInput.z));
        animator.SetFloat("MovementSpeed", maxMovement);
    }
}