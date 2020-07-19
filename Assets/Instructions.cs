using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public Text instructions;
    void Start()
    {
        instructions.text = "WELCOME TO YOUR CHILDHOOD MEMORY BEING \nREVIVED IN AUGMENTED REALITY FORM\n 1.Point your phone towards\n a floor and wait till you see a mesh appearing \n 2.Click on " +
            "the virtual mesh \n 3.Pinch to zoom in or out in order\n to vary the size of the snake\n 4.Eat the available food items\n to grow the snake \n 5.The game ends once the snake body\n touches itself BEWARE!!!! \n 6.Touch on the screen\n to " +
            "start playing! \n\n\nHope you'll have an amazing \nEXPERIENCE!!";
    }
    public void BtnStartGame()
    {
        SceneManager.LoadScene("SnakeGame");
    }
 }
