using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegRightSideUp : MonoBehaviour
{
    #region Variables
    private GameObject player;

    private bool freezeVegPos = false;
    #endregion



    private void Start()
    {
        // Sets player as main camera
        player = GameObject.Find("MainCamera");
    }

    private void Update()
    {
        if (freezeVegPos == true)
        {
            // Vegetable gets frozen in place
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            // Disallows vegetable constraints to get frozen
            freezeVegPos = false;
            // Vegetable constraints get unfrozen
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if vegetable collides with floor
        if (collision.gameObject.tag == "Floor")
        {
            // Vegetable will stand right side up
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            // Facing towards player
            gameObject.GetComponent<Transform>().LookAt(player.GetComponent<Transform>());
            // Allows vegetable constraints to get frozen
            freezeVegPos = true;
        }
    }
}
