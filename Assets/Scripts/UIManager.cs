using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using UnityEngine.SceneManagement;

public class UIManager: MonoBehaviour
{
    public Text appleCountText;
    public Text bananaCountText;
    public Text pizzaCountText;
    public GameObject endGameUI;
    public Text endUIText;
    public GameObject reloadButton;

    public RectTransform scorePanel;
    public RectTransform panelParent;
    public RectTransform bodyParent;

    int appleCount;
    int bananaCount;
    int pizzaCount;

    string bodyBite = "That turn was pretty steep.\n" +
    "The Snake bit itself.\n\n" +
    "Tap Anywhere to play again!";

    string bombBite = "Ah, our poor snake\ncan't digest a BOMB \n\n" +
         "Tap Anywhere to play again!";

    private void OnEnable()
    {
        FoodConsumer.SelfAnnihilation += ShowGameOverUI;
        SceneController.PlaneSelected += GameRestarted;
        FoodConsumer.FoodConsumed += FoodCountUpdate;
    }

    private void OnDisable()
    {
        FoodConsumer.SelfAnnihilation -= ShowGameOverUI;
        SceneController.PlaneSelected -= GameRestarted;
        FoodConsumer.FoodConsumed -= FoodCountUpdate;
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

    void ShowGameOverUI(BiteType biteType)
    { 
        //endUIText.text = biteType == BiteType.Body ? bodyBite : bombBite;

        if (biteType == BiteType.Body)
        {
            endUIText.text = bodyBite;
        }
        else
        {
            endUIText.text = bombBite;
        }

        endGameUI.SetActive(true);
        reloadButton.SetActive(false);
        scorePanel.SetParent(bodyParent);

        scorePanel.anchorMin = new Vector2(0.5f, 1f);
        scorePanel.anchorMax = new Vector2(0.5f, 1f);
        scorePanel.pivot = new Vector2(0.5f, 1f);
        scorePanel.localPosition = new Vector3(0, 150, 0);
    }

    void GameRestarted(DetectedPlane detectedPlane)
    {
        endGameUI.SetActive(false);
        reloadButton.SetActive(true);

        appleCount = 0; bananaCount = 0; pizzaCount = 0;
        CounterTextUpdate();

        scorePanel.SetParent(panelParent);

        scorePanel.anchorMin = new Vector2(0f, 1f);
        scorePanel.anchorMax = new Vector2(0f, 1f);
        scorePanel.pivot = new Vector2(0f, 1f);
        scorePanel.localPosition = Vector3.zero;
    }

    void CounterTextUpdate()
    {
        appleCountText.text = appleCount.ToString();
        bananaCountText.text = bananaCount.ToString();
        pizzaCountText.text = pizzaCount.ToString();
    }

    public void ReloadCurrentScene()
    {
        SceneController.playing = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
