using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementsMove : MonoBehaviour
{
    public Vector3 destination;
    public float time;

    protected virtual void Start()
    {
        MoveElement();
    }

    protected virtual void MoveElement()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", destination,
            "islocal", true,
            "time", time));
    }
}
