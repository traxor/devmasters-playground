using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // FixedUpdate is called at a fixed interval independently of frame rate.
    void FixedUpdate()
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
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
