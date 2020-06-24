using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegBehaviour : MonoBehaviour
{
    #region Variables
    private GameObject player;

    private float playerDistance;

    private bool lookingAtPlayer = false;
    #endregion



    private void Start()
    {
        player = GameObject.Find("MainCamera");
    }

    private void Update()
    {
        playerDistance = gameObject.GetComponent<PickThrow>().Distance;

        if (playerDistance <= 3f && !lookingAtPlayer)
        {
            gameObject.GetComponent<Transform>().LookAt(player.GetComponent<Transform>());
        }

        if (playerDistance > 3f)
        {
            lookingAtPlayer = false;
        }
    }
}
