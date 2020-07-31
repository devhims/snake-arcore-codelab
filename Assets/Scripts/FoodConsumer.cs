using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleARCore;

public class FoodConsumer : MonoBehaviour
{
    public static event Action SelfAnnihilation;
    public static event Action<string> FoodConsumed;

    public AudioClip chewingClip;
    public AudioClip bombClip;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            collision.gameObject.SetActive(false);

            Slithering s = GetComponentInParent<Slithering>();
            if (s != null)
            {
                s.AddBodyPart();
            }

            audioSource?.PlayOneShot(chewingClip);
            FoodConsumed?.Invoke(collision.gameObject.name);
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            audioSource?.PlayOneShot(bombClip);
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        SceneController.playing = false;
        SelfAnnihilation?.Invoke();
    }
}
