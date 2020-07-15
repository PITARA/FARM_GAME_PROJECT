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

    private bool isDecaying;
    public bool isStarving { get; private set; }

    public bool canFeed { get; private set; }

    public Quality vegQuality { get; private set; }

    private Dew dew = new Dew();

    #endregion



    public enum Quality
    {
        DECAYING, // Less than 0
        NORMAL, // 0
        GOOD, // 25
        GREAT, // 50
        EXCELLENT, // 75
        PRISTINE // 100
    }

    private void Start()
    {
        isDecaying = false;
        canFeed = false;
        isStarving = false;

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
            vegQuality = Quality.DECAYING;
        }

        if (vegActualQuality >= 0 && vegActualQuality <= 24)
        {
            vegQuality = Quality.NORMAL;
        }

        if (vegActualQuality >= 25 && vegActualQuality <= 49)
        {
            vegQuality = Quality.GOOD;
        }

        if (vegActualQuality >= 50 && vegActualQuality <= 74)
        {
            vegQuality = Quality.GREAT;
        }

        if (vegActualQuality >= 75 && vegActualQuality <= 99)
        {
            vegQuality = Quality.EXCELLENT;
        }

        if (vegActualQuality >= 100)
        {
            vegQuality = Quality.PRISTINE;
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

            isStarving = false;

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

            isStarving = true;
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
