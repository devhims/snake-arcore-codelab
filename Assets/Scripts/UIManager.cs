using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class UIManager: MonoBehaviour
{
    public Text appleCountText;
    public Text bananaCountText;
    public Text pizzaCountText;
    public GameObject endGameUI;

    int appleCount;
    int bananaCount;
    int pizzaCount;

    private void OnEnable()
    {
        FoodConsumer.SelfAnnihilation += BodyBite;
        SceneController.PlaneSelected += GameRestarted;
        FoodConsumer.FoodConsumed += FoodCountUpdate;
    }

    void Start()
    {
        endGameUI.SetActive(false);
    }

    void FoodCountUpdate(string name)
    {
        if (name == "Apple")
        {
            appleCount++;
            CounterTextUpdate();
        }
        else if (name == "Banana")
        {
            bananaCount++;
            CounterTextUpdate();
        }
        else if (name == "Pizza")
        {
            pizzaCount++;
            CounterTextUpdate();
        }
        else
        {
            Debug.LogWarning("Unknown food consumed: " + name);
        }
    }

    void BodyBite()
    {
        endGameUI.SetActive(true);
    }

    void GameRestarted(DetectedPlane detectedPlane)
    {
        endGameUI.SetActive(false);

        appleCount = 0; bananaCount = 0; pizzaCount = 0;
        CounterTextUpdate();
    }

    void CounterTextUpdate()
    {
        appleCountText.text = appleCount.ToString();
        bananaCountText.text = bananaCount.ToString();
        pizzaCountText.text = pizzaCount.ToString();
    }
}
