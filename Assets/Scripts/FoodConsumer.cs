using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FoodConsumer : MonoBehaviour
{
    public static event Action SelfAnnihilation;
    public static int ap = 0 , ba = 0 , pi = 0 ;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            collision.gameObject.SetActive(false);
            Slithering s = GetComponentInParent<Slithering>();

            if (s != null)
            {
                s.AddBodyPart();
            }

            ap += 1 ;
        }
        else if (collision.gameObject.tag == "Banana")
        {
            
            collision.gameObject.SetActive(false);
            Slithering s = GetComponentInParent<Slithering>();

            if (s != null)
            {
                s.AddBodyPart();
            }
            ba += 1;
        }
        else if (collision.gameObject.tag == "Pizza")
        {
            
            collision.gameObject.SetActive(false);
            Slithering s = GetComponentInParent<Slithering>();

            if (s != null)
            {
                s.AddBodyPart();
            }
            pi += 1 ;
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            Time.timeScale = 0;
            SceneController.playing = false;
            SelfAnnihilation?.Invoke();
        }

    }
}
