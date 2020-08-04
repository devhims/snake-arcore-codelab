using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiddleMove : UIElementsMove
{
    public bool finishedMove = false;

    protected override void Start()
    {
        this.MoveElement();
    }

    protected override void MoveElement()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", destination, "islocal", true, "time", time, "oncomplete", "SetBool", "easetype", iTween.EaseType.easeOutBack));
    }

    void SetBool()
    {
        finishedMove = true;
    }

}
