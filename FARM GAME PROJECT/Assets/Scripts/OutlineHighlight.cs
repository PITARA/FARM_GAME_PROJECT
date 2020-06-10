using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHighlight : MonoBehaviour
{
    #region Variables
    private Renderer rend;

    private GameObject playerHand;

    private float defaultOutline = 1.0f;
    private float highlightedOutline = 1.05f;
    private float distance;

    private bool isHolding;
    #endregion



    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Outline");

        rend.material.SetFloat("_OutlineWidth", defaultOutline);

        playerHand = GameObject.FindWithTag("PlayerHand");
    }

    private void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, playerHand.transform.position);
        isHolding = gameObject.GetComponent<PickThrow>().isHolding;
    }

    private void OnMouseOver()
    {
        if (distance <= 3f)
        {
            if (isHolding)
            {
                rend.material.SetFloat("_OutlineWidth", defaultOutline);
            }
            else
            {
                rend.material.SetFloat("_OutlineWidth", highlightedOutline);
            }
        }
    }

    private void OnMouseExit()
    {
        rend.material.SetFloat("_OutlineWidth", defaultOutline);
    }
}
