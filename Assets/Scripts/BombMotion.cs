using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BombMotion : FoodMotion
{
    public AudioClip bombBlast;
    public GameObject smokeObject;

    public static event Action<AudioClip> BombDead;
    bool canBlast = true;

    protected override void OnEnable()
    {
        FoodConsumer.SelfAnnihilation += SetGameOver;
        canBlast = true;
        age = Random.Range(2, 6);
        base.OnEnable();
    }

    private void OnDisable()
    {
        FoodConsumer.SelfAnnihilation -= SetGameOver;
        if (canBlast)
        {
            smokeObject.transform.position = transform.position;
            smokeObject.SetActive(true);          
            BombDead?.Invoke(bombBlast);
        }
    }

    void SetGameOver(BiteType biteType)
    {
        canBlast = false;
    }
}
