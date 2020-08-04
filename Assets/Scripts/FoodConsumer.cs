using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleARCore;

public enum BiteType
{
    Body,
    Bomb
}

[RequireComponent(typeof(AudioSource))]
public class FoodConsumer : MonoBehaviour
{
    public static event Action<BiteType> SelfAnnihilation;
    public static event Action<string> FoodConsumed;

    public AudioClip chewingClip;
    public AudioClip bombClip;
    public AudioClip bodyBiteClip;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            collision.gameObject.SetActive(false);

            Slithering s = GetComponentInParent<Slithering>();
            if (s != null)
            {
                s.AddBodyPart();
            }

            audioSource.PlayOneShot(chewingClip);
            FoodConsumed?.Invoke(collision.gameObject.name);
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            audioSource.PlayOneShot(bombClip);
            SelfAnnihilation?.Invoke(BiteType.Bomb);
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            audioSource.PlayOneShot(bodyBiteClip);
            SelfAnnihilation?.Invoke(BiteType.Body);
        }
    }
}
