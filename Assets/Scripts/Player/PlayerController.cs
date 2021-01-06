using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1;

    private Rigidbody body;
 
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        if (body == null)
        {
            Debug.LogError("Failed to get Rigidbody component!");
            return;
        }
    }

    // Update is called for each frame
    void Update()
    {
        MovePlayer();
    }

    /// <summary>
    /// To be executed in update. Moves the player according to current input.
    /// </summary>
    void MovePlayer()
    {
        // Read player input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate new movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Move the player object
        body.AddForce(movement * moveSpeed);
    }
}
