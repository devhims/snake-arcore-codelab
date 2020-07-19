using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class FoodConsumer : MonoBehaviour
{
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
    }
}
