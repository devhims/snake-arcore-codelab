using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject instructionPanel;

    public void ShowInstructions()
    {
        startPanel.SetActive(false);
        instructionPanel.SetActive(true);
    }

    public void LoadARScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
