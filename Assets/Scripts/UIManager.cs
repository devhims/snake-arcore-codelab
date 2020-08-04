using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using UnityEngine.SceneManagement;
using LevelManagement.Data;

public class UIManager: MonoBehaviour
{
    public Text appleCountText;
    public Text bananaCountText;
    public Text pizzaCountText;
    public GameObject endGameUI;
    public Text endUIText;
    public GameObject reloadButton;
    public GameObject reloadPanel;
    public GameObject highScoreUI;
    public GameObject handScanUI;

    public RectTransform scorePanel;
    public GameObject scoreCounterUI;
    public RectTransform panelParent;
    public RectTransform bodyParent;

    public SnakeController snakeController;

    int appleCount;
    int bananaCount;
    int pizzaCount;
    int score = 0;

    string bodyBite = "That turn was pretty steep.\n" +
    "The Snake bit itself.\n\n" +
    "Tap Anywhere to play again!";

    string bombBite = "Ah, our poor snake\ncan't digest a BOMB \n\n" +
         "Tap Anywhere to play again!";

    private DataManager dataManager;

    private void OnEnable()
    {
        FoodConsumer.SelfAnnihilation += ShowGameOverUI;
        SceneController.PlaneSelected += GameRestarted;
        FoodConsumer.FoodConsumed += FoodCountUpdate;

        dataManager = FindObjectOfType<DataManager>();
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
        score++;

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

        dataManager.Load();

        if (score > dataManager.HighScore)
        {
            dataManager.HighScore = score;
            dataManager.Save();
            highScoreUI.SetActive(true);
        }
        else
        {
            highScoreUI.SetActive(false);
        }

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

        scoreCounterUI.SetActive(true);
        scoreCounterUI.transform.GetChild(0).GetComponent<Text>().text = string.Format("Score = {0}", score);

        scorePanel.SetParent(bodyParent);
        scorePanel.localPosition = Vector3.zero;
    }

    void GameRestarted(DetectedPlane detectedPlane)
    {
        endGameUI.SetActive(false);
        reloadButton.SetActive(true);
        scoreCounterUI.SetActive(false);
        handScanUI.SetActive(false);
        panelParent.gameObject.SetActive(true);

        appleCount = 0; bananaCount = 0; pizzaCount = 0;
        CounterTextUpdate();

        scorePanel.SetParent(panelParent);
        scorePanel.localPosition = Vector3.zero;

        score = 0;
    }

    void CounterTextUpdate()
    {
        appleCountText.text = appleCount.ToString();
        bananaCountText.text = bananaCount.ToString();
        pizzaCountText.text = pizzaCount.ToString();
    }

    public void ReloadCurrentScene()
    {
        reloadPanel.SetActive(true);
        reloadButton.SetActive(false);
        scorePanel.gameObject.SetActive(false);
        handScanUI.SetActive(false);
        SceneController.playing = false;
        StartCoroutine(LevelReloadRoutine());

        snakeController.gameObject.SetActive(false);
        score = 0;
    }

    IEnumerator LevelReloadRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
