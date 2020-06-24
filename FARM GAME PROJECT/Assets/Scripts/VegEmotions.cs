using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegEmotions : MonoBehaviour
{
    #region Variables
    public Sprite[] emotion = new Sprite[5];

    public Image face;

    private bool isHolding;
    private bool doWink = false;

    private float winkAnimationTime = 0.5f;
    private float winkAnimationTimer;
    #endregion


    private void Start()
    {
        winkAnimationTimer = winkAnimationTime;
    }

    private void Update()
    {
        isHolding = gameObject.GetComponent<PickThrow>().isHolding;

        if (isHolding)
        {
            GettingHeldEmotion();
        }
        else
        {
            if (!doWink)
            {
                NormalEmotion();
            }
        }

        // 
        if (doWink)
        {
            winkAnimationTimer -= Time.deltaTime;
            if (winkAnimationTimer <= 0)
            {
                doWink = false;
                winkAnimationTimer = winkAnimationTime;
            }
        }
    }

    private void GettingHeldEmotion()
    {
        if (face.sprite != emotion[2])
        {
            face.sprite = emotion[2];
        }
    }

    private void NormalEmotion()
    {
        if (face.sprite != emotion[0])
        {
            face.sprite = emotion[0];
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            face.sprite = emotion[1];
            doWink = true;
        }
    }
}
