using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegRightSideUp : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("MainCamera");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if vegetable collides with floor
        if (collision.gameObject.tag == "Floor")
        {
            // Vegetable will stand right side up
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Transform>().LookAt(player.GetComponent<Transform>());
        }
    }
}
