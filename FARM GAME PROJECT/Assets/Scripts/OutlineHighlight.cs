using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHighlight : MonoBehaviour
{
    #region Variables
    private Renderer rend;

    private GameObject playerHand;

    // Width of the outlines
    private float defaultOutline = 1.0f;
    private float highlightedOutline = 1.05f;

    // Distance that the player can interact with the object
    private float distance;

    private bool isHolding;
    #endregion



    void Start()
    {
        // Finds renderer on interactable object
        rend = GetComponentInChildren<Renderer>();
        // Finda shader on interactable object
        rend.material.shader = Shader.Find("Outline");

        rend.material.SetFloat("_OutlineWidth", defaultOutline);

        playerHand = GameObject.FindWithTag("PlayerHand");
    }

    private void Update()
    {
        // Gets distance between interactable object and player hand
        distance = Vector3.Distance(gameObject.transform.position, playerHand.transform.position);
        // Gets info if player is holding interactable object or not
        isHolding = gameObject.GetComponent<PickThrow>().isHolding;
    }

    private void OnMouseOver()
    {
        if (distance <= 3f)
        {
            // If player is holding object
            if (isHolding)
            {
                // Object is not highlighted
                rend.material.SetFloat("_OutlineWidth", defaultOutline);
            }
            // If player is not holding object
            else
            {   // Object gets highlighted when mouse is over it
                rend.material.SetFloat("_OutlineWidth", highlightedOutline);
            }
        }
    }

    private void OnMouseExit()
    {
        // Object is not highlighted
        rend.material.SetFloat("_OutlineWidth", defaultOutline);
    }
}
