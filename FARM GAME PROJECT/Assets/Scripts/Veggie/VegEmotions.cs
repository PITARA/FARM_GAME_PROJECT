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

    public AudioSource veggieHittingFloor_AudioSource;
    public AudioSource veggieThrown_AudioSource;

    public AudioClip veggieHittingFloor_Clip;
    public AudioClip[] veggieThrown_Clip = new AudioClip[3];

    #endregion


    private void Start()
    {
        winkAnimationTimer = winkAnimationTime;
    }

    private void Update()
    {
        isHolding = gameObject.GetComponent<PickThrow>().isHolding;
        Debug.Log("Is holding: " + isHolding);

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            face.sprite = emotion[1];
            doWink = true;
            PlaySound(veggieHittingFloor_Clip);
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

    private void PlaySound(AudioClip clip)
    {
        AudioClip chosenAudioClip = clip;

        switch (chosenAudioClip.name)
        {
            case "veggieHittingFloor_SFX":
                veggieHittingFloor_AudioSource.clip = clip;
                veggieHittingFloor_AudioSource.Play();
                break;
            default:
                veggieThrown_AudioSource.clip = clip;
                veggieThrown_AudioSource.Play();
                break;
        }
    }

    private AudioClip RandVeggieThrownClip()
    {
        AudioClip randClip = null;

        int randomRange = Random.Range(1, 4);

        switch (randomRange)
        {
            case 1:
                randClip = veggieThrown_Clip[0];
                break;
            case 2:
                randClip = veggieThrown_Clip[1];
                break;
            case 3:
                randClip = veggieThrown_Clip[2];
                break;
            default:
                Debug.LogError("That veggie being thrown sound does not exist.");
                break;
        }

        return randClip;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isHolding)
            {
                PlaySound(RandVeggieThrownClip());
            }
        }
    }
}
