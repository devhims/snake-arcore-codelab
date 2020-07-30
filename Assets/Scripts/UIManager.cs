using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class UIManager: MonoBehaviour
{
    public Text appleCount;
    public Text bananaCount;
    public Text pizzaCount;

    public GameObject endGameUI;

    private void OnEnable()
    {
        FoodConsumer.SelfAnnihilation += BodyBite;
        SceneController.PlaneSelected += GameRestarted;
    }

    void Start()
    {
        appleCount.text = " X 0 ";
        bananaCount.text = " X 0 ";
        pizzaCount.text = " X 0 ";

        endGameUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        appleCount.text = " X " + FoodConsumer.ap;
        bananaCount.text = " X " + FoodConsumer.ba;
        pizzaCount.text = " X " + FoodConsumer.pi;
    }

    void BodyBite()
    {
        endGameUI.SetActive(true);
    }

    void GameRestarted(DetectedPlane detectedPlane)
    {
        endGameUI.SetActive(false);
    }
}
