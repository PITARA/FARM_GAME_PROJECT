using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegQuality : MonoBehaviour
{
    #region Variables
    private int vegDefaultQuality = 0;
    private int vegActualQuality;

    private float timeLoop_Delay = 1;
    private float timeLoop_Time;

    private float feedingCooldown = 10;
    private float feedingTime = 0;

    private float decayingCooldown = 10;
    private float decayingTime = 0;

    private bool isDecaying = false;

    private bool canFeed = false;

    private Quality vegQuality;

    private Dew dew = new Dew();

    #endregion



    enum Quality
    {
        Decaying, // Less than 0
        Normal, // 0
        Good, // 25
        Great, // 50
        Excellent, // 75
        Pristine // 100
    }

    private void Start()
    {
        vegActualQuality = vegDefaultQuality;
        feedingTime = Time.time + feedingCooldown;
    }

    void Update()
    {
        VegQualityCheck();
        Debug.Log("VEGGIE QUALITY: " + vegActualQuality + vegQuality);

        // If cooldown for feeding vegetables has ended
        if (Time.time > feedingTime)
        {
            if (!canFeed)
            {
                // Feeding is enabled
                canFeed = true;
            }
        }

        // If veggie is hungry
        if (canFeed)
        {
            // And VegDecaying() is not in cooldown
            if (Time.time > decayingTime)
            {
                VegDecaying();
            }
        }

        /*if (timeLoop_Time > 0)
        {
            timeLoop_Time -= Time.deltaTime;

            if (timeLoop_Time < 0) timeLoop_Time = 0;
        }

        if (timeLoop_Time == 0)
        {
            // Colocar aqui as funções
            timeLoop_Time = timeLoop_Delay;
        }*/
    }

    private void VegQualityCheck()
    {
        if (vegActualQuality < 0)
        {
            vegQuality = Quality.Decaying;
            Debug.Log("A");
        }

        if (vegActualQuality >= 0 && vegActualQuality <= 24)
        {
            vegQuality = Quality.Normal;
            Debug.Log("B");
        }

        if (vegActualQuality >= 25 && vegActualQuality <= 49)
        {
            vegQuality = Quality.Good;
            Debug.Log("C");
        }

        if (vegActualQuality >= 50 && vegActualQuality <= 74)
        {
            vegQuality = Quality.Great;
            Debug.Log("D");
        }

        if (vegActualQuality >= 75 && vegActualQuality <= 99)
        {
            vegQuality = Quality.Excellent;
            Debug.Log("E");
        }

        if (vegActualQuality >= 100)
        {
            vegQuality = Quality.Pristine;
            Debug.Log("F");
        }
    }

    public void VegFeeding()
    {
        // If veggie is hungry
        if (canFeed)
        {
            // And quality is normal or above
            if (vegActualQuality >= 0)
            {
                // Veggie gets fed and increases its quality
                vegActualQuality += dew.DewFeedValue;
            }

            // If veggie quality is decaying(under zero quality)
            if (vegActualQuality < 0)
            {
                // Veggie gets fed, but doesn't increase its quality
                vegActualQuality = 0;
            }

            // Cooldown for getting fed again starts
            feedingTime = Time.time + feedingCooldown;

            // Disables feeding
            canFeed = false;

            // Disables decay
            isDecaying = false;

            // Makes eating face
            gameObject.GetComponent<VegEmotions>().GettingFedEmotion();
        }
        else
        {
            // ADD UI NOTICE "VEGGIE NOT HUNGRY"
            Debug.Log("O vegetal não está com fome");
        }
    }

    public void VegDecaying()
    {
        // If veggie is decaying
        if (isDecaying)
        {
            // Veggie quality goes down
            vegActualQuality -= dew.DewFeedValue;
            // Decaying enters cooldown before quality goes down again
            decayingTime = Time.time + decayingCooldown;
        }

        // Fist time veggie gets hungry it gets a decaying tag without losing quality, but if loop completes and veggie is still hungry, veggie starts losing quality
        isDecaying = true;
        // Cooldown starts
        decayingTime = Time.time + decayingCooldown;
    }

    public bool CanFeed
    {
        get
        {
            return canFeed;
        }
    }
}
