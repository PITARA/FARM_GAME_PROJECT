using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegEmotions : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Sprite[] emotion = new Sprite[7]; // [0 = Normal], [1 = Normal_Wink], [2 = Hold], [3 = Hungry], [4 = Panic], [5 = Hungry_PickedUp], [6 = GettingFed]

    public Image face;

    private bool isHolding;
    private bool doWink = false;
    private bool gettingFedEmotion = false;

    private float animationTime = 0.5f;
    private float winkAnimationTimer;

    private float gettingFedAnimationTimer;

    public AudioSource veggieHittingFloor_AudioSource;
    public AudioSource veggieThrown_AudioSource;
    public AudioSource veggieGettingFed_AudioSource;

    public AudioClip veggieHittingFloor_Clip;
    public AudioClip[] veggieThrown_Clip = new AudioClip[3];
    [SerializeField]
    private AudioClip[] veggieGettingFed_Clip = new AudioClip[2];

    #endregion


    private void Start()
    {
        winkAnimationTimer = animationTime;
        gettingFedAnimationTimer = animationTime;
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
            if (!doWink && !gettingFedEmotion)
            {
                NormalEmotion();
            }
        }

        // Stays for 0.5 seconds on wink face
        if (doWink)
        {
            winkAnimationTimer -= Time.deltaTime;
            if (winkAnimationTimer <= 0)
            {
                doWink = false;
                winkAnimationTimer = animationTime;
            }
        }

        // Stays for 0.5 seconds on fed face
        if (gettingFedEmotion)
        {
            gettingFedAnimationTimer -= Time.deltaTime;
            if (gettingFedAnimationTimer <= 0)
            {
                gettingFedEmotion = false;
                gettingFedAnimationTimer = animationTime;
            }
        }

        if (gameObject.GetComponent<VegQuality>().CanFeed)
        {
            if (!isHolding)
            {
                HungryEmotion();
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
        if (gameObject.GetComponent<VegQuality>().CanFeed)
        {
            if (face.sprite != emotion[5])
            {
                face.sprite = emotion[5];
            }
        }
        else
        {
            if (face.sprite != emotion[2])
            {
                face.sprite = emotion[2];
            }
        }
    }

    private void NormalEmotion()
    {
        if (face.sprite != emotion[0])
        {
            face.sprite = emotion[0];
        }
    }

    private void HungryEmotion()
    {
        if (face.sprite != emotion[3])
        {
            face.sprite = emotion[3];
        }
    }

    public void GettingFedEmotion()
    {
        if (face.sprite != emotion[6])
        {
            face.sprite = emotion[6];
            gettingFedEmotion = true;
            PlaySound(RandVeggieFedClip());
        }
    }

    public void PlaySound(AudioClip clip)
    {
        AudioClip chosenAudioClip = clip;

        switch (chosenAudioClip.name)
        {
            case "veggieHittingFloor_SFX":
                veggieHittingFloor_AudioSource.clip = clip;
                veggieHittingFloor_AudioSource.Play();
                break;
            case "veggieGettingFed01_SFX":
            case "veggieGettingFed02_SFX":
                veggieGettingFed_AudioSource.clip = clip;
                veggieGettingFed_AudioSource.Play();
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

    public AudioClip RandVeggieFedClip()
    {
        AudioClip randFedClip = null;

        int randomRange = Random.Range(1, 3);

        switch (randomRange)
        {
            case 1:
                randFedClip = veggieGettingFed_Clip[0];
                break;
            case 2:
                randFedClip = veggieGettingFed_Clip[1];
                break;
            default:
                Debug.LogError("That veggie being thrown sound does not exist.");
                break;
        }

        return randFedClip;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isHolding)
            {
                if (!gameObject.GetComponent<VegQuality>().CanFeed)
                {
                    PlaySound(RandVeggieThrownClip());
                }
            }
        }
    }
}
