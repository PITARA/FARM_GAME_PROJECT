using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionVegetable : MonoBehaviour
{
    #region Variables
    private Renderer rend;

    private float defaultOutline = 1.0f;
    private float highlightedOutline = 1.05f;
    #endregion



    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Outline");
    }

    private void OnMouseEnter()
    {
        rend.material.SetFloat("_OutlineWidth", highlightedOutline);
    }

    private void OnMouseExit()
    {
        rend.material.SetFloat("_OutlineWidth", defaultOutline);
    }
}
