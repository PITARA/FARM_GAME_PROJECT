using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickThrow : MonoBehaviour
{
    #region Variables
    private float throwForce = 600;
    // Distance that the player can interact with the object
    public float distance { get; private set; }

    private Vector3 objectPos;

    private GameObject playerHand;
    private GameObject player;

    public bool canHold = true;
    public bool isHolding = false;
    #endregion



    private void Start()
    {
        playerHand = GameObject.FindWithTag("PlayerHand");
        player = GameObject.Find("MainCamera");
    }

    void Update()
    {
        // Gets distance between interactable object and player hand
        distance = Vector3.Distance(gameObject.transform.position, playerHand.transform.position);

        // If interactable object is out of range
        if (distance >= 3f)
        {
            isHolding = false;
        }
        
        // If player is holding object
        if (isHolding == true)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            // Interactable object becomes child of player hand
            gameObject.transform.SetParent(playerHand.transform);

            // If right mouse button is pressed
            if (Input.GetMouseButtonDown(1))
            {
                // Interactable object is thrown
                gameObject.GetComponent<Rigidbody>().AddForce(playerHand.transform.forward * throwForce);
                isHolding = false;
            }
        }
        // If player is not holding object
        else
        {
            objectPos = gameObject.transform.position;
            gameObject.transform.SetParent(null);
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.transform.position = objectPos;
        }
    }

    void OnMouseDown()
    {
        // If interactable object is within range
        if (distance <= 3f)
        {
            isHolding = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            gameObject.GetComponent<Transform>().LookAt(player.GetComponent<Transform>());
        }
    }
    void OnMouseUp()
    {
        isHolding = false;
    }

    public float Distance
    {
        get { return distance; }
    }
}
