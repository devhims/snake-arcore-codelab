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

            FoodConsumed?.Invoke(collision.gameObject.name);
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            Time.timeScale = 0;
            SceneController.playing = false;
            SelfAnnihilation?.Invoke();
        }
    }
}
