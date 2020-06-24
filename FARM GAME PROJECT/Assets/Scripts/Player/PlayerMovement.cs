using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    // Stores object with component 'Character Controller'
    private CharacterController controller;

    // Stores object 'PlayerGroundCheck'
    private Transform groundCheck;

    // Controls what objects 'PlayerGroundCheck' should check for
    private LayerMask groundMask;

    // Controls speed of the player movement
    private float movementSpeed = 12f;
    // Controls gravity of the player movement
    private float gravity = -9.81f * 4;
    // Radious of GroundCheck sphere
    private float groundDistance = 0.4f;
    // Controls player jump height
    private float jumpHeight = 3f;

    // Indicates if player is grounded or not
    private bool isGrounded;

    // Stores current player physics velocity
    private Vector3 velocity;
    #endregion


    private void Start()
    {
        // Sets component 'Character Controller' of 'Player Entity' to variable
        controller = gameObject.GetComponent<CharacterController>();

        // Sets component 'Transform' of 'PlayerGroundCkeck' to variable
        groundCheck = this.gameObject.transform.Find("PlayerGroundCheck");

        // Sets 'groundMask' with the layer 'Ground'
        groundMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        // Creates sphere to check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If player is grounded and physiscs pull is still happening
        if (isGrounded && velocity.y < 0)
        {
            // Physics pull 'resets'
            velocity.y = -2f;
        }

        // Gets input axis for 'wasd' movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Sets desired direction of movement based on xz movement and the direction the player is facing
        Vector3 movement = transform.right * x + transform.forward * z;

        // Moves 'Player Entity' on the previously defined direction and speed
        controller.Move(movement * movementSpeed * Time.deltaTime);

        // If player tried to jump and is grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Player jumps
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Sets current velocity to variable
        velocity.y += gravity * Time.deltaTime;

        // Gives physics pull to 'Player Entity' to prevent hovering
        controller.Move(velocity* Time.deltaTime);


    }
}
