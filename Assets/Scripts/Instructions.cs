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
        instructions.text = "1. Click on Start and give\n camera access when prompted\n for enabling AR features.\n" +
            "2. Scan the floor till\n you see a virtual mesh.\n" +
            "3. Click on the virtual mesh to\n place the snake and food.\n " +
            "4. Pinch to zoom in or out in order\n to vary the size of the snake.\n " +
            "5. Eat the available food items\n to grow the snake.\n" +
            "6. The game ends once the snake\n body touches itself. BEWARE!!!";
    }
 }
