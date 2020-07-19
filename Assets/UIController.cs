using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text appleCount;
    public Text bananaCount;
    public Text pizzaCount;
  
    void Start()
    {
        appleCount.text = " X 0 ";
        bananaCount.text = " X 0 ";
        pizzaCount.text = " X 0 "; 
    }
    // Update is called once per frame
    void Update()
    {
        appleCount.text = " X " + FoodConsumer.ap;
        bananaCount.text = " X " + FoodConsumer.ba;
        pizzaCount.text = " X " + FoodConsumer.pi;
    }
}
