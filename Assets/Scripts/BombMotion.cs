using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMotion : FoodMotion
{
    protected override void OnEnable()
    {
        age = Random.Range(3, 8);
        base.OnEnable();
    }
}
