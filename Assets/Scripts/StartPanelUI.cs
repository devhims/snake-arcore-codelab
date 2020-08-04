using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelUI : MonoBehaviour
{
    public UIMiddleMove MiddleMove;
    public UIElementsMove[] ElementsMove;

    bool moveEnabled;

    // Update is called once per frame
    void Update()
    {
        if (MiddleMove.finishedMove && !moveEnabled)
        {
            moveEnabled = true;
            foreach (var go in ElementsMove)
            {
                go.gameObject.SetActive(true);
            }
        }
    }
}
