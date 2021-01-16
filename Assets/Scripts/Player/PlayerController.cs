using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    private Vector3 playerVelocity = Vector3.zero;
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
    }

    /// <summary>
    /// To be executed in update. Moves the character according to current input.
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
        Vector3 movementSpeed;

        if (isRunning)
        {
            movementSpeed = movementDirection * runSpeed;
        }
        else
        {
            movementSpeed = movementDirection * walkSpeed;
        }
        SetMovementAnimation(movementSpeed);
        characterController.Move(movementSpeed * Time.deltaTime);

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
    /// Gets the maximum magnitude of the movement directions to decide if
    /// the character should be running, walking, or standing idle.
    /// </summary>
    /// <param name="movementSpeed"></param>
    void SetMovementAnimation(Vector3 movementSpeed)
    {
        float maxMovement = Mathf.Max(Mathf.Abs(movementSpeed.x),
                                      Mathf.Abs(movementSpeed.y));
        if (maxMovement >= runSpeed)
        {
            // Running
            animator.SetTrigger("isRunning");
            animator.SetTrigger("isWalking");
        }
        else if (maxMovement < runSpeed && maxMovement > 0)
        {
            // Walking
            animator.SetTrigger("isWalking");
            animator.SetTrigger("isRunning");
        }
        else
        {
            // Idle
            animator.SetTrigger("isWalking");
            animator.SetTrigger("isRunning");
        }
    }
}