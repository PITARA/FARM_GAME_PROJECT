using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Incubator : MonoBehaviour
{
    #region Variables
    // Aesthetic only prefab
    public GameObject seedPrefab;

    public GameObject vegPrefab;
    public GameObject vegSpawnPoint;

    public Image progressBar;

    private GameObject seedInstance;

    // Shows if incubator is being used
    private bool isFull = false;

    // How long it takes for vegetable to eclode
    private float timeAmt = 10;
    private float time;
    #endregion



    private void Start()
    {
        // Ser progress of eclosion to zero
        progressBar.fillAmount = 0;
        // Sets how long it takes to vegetable to eclode
        time = timeAmt;
    }

    private void Update()
    {
        // If there's a seed on the incubator
        if (isFull == true)
        {
            // If progress on incubator is not zero
            if (time > 0)
            {
                // Countdown starts
                time -= Time.deltaTime;
                // Progress bar starts to fill down
                progressBar.fillAmount = time / timeAmt;
            }
            // If progress is done
            if (time <= 0)
            {
                // Aesthetic seed is destroyed
                Destroy(seedInstance);
                // Incubator is set to empty
                isFull = false;
                // Time on the progress bar is reset
                time = timeAmt;
                // A vegetable is spawned
                Instantiate(vegPrefab, vegSpawnPoint.transform.position, vegSpawnPoint.transform.rotation);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If gameobject is a seed
        if (collision.gameObject.tag == "Seed")
        {
            // Seed gameobject gets destroyed
            Destroy(collision.gameObject);

            isFull = true;

            // Aesthetic only seed prefab spawns inside incubator
            seedInstance = Instantiate(seedPrefab, transform.position, transform.rotation);
            Debug.Log("Semente entrou na incubadora");
        }

        // If gameobject is not a seed
        else
        {
            Debug.Log("Isso não é uma semente!");
        }
    }
}
