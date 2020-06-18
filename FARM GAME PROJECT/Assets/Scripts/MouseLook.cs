using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Variables
    // Reference to whole player 
    public Transform playerBody;

    // Controls velocity of mouse look
    private float mouseSensitivity = 500f;

    // Controls player body rotation
    private float xRotation = 0f;
    #endregion



    private void Start()
    {
        // Hides and lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Axis that are going to change based on the player mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotates whole player based on Mouse X variable
        playerBody.Rotate(Vector3.up * mouseX);

        // Changes rotation variable based on player Mouse Y movement
        xRotation -= mouseY;

        // Stops over rotation to look behind the player
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotates camera based on xRotation variable
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
