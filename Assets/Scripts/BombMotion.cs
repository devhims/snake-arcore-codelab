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

    public bool gameOverCondition;

    protected override void OnEnable()
    {
        FoodConsumer.SelfAnnihilation += SetGameOver;
        gameOverCondition = false;
        age = Random.Range(2, 6);
        base.OnEnable();
    }

    private void OnDisable()
    {
        FoodConsumer.SelfAnnihilation -= SetGameOver;
        if (!gameOverCondition)
        {
            smokeObject.transform.position = transform.position;
            smokeObject.SetActive(true);
            smokeObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(RemoveSmoke());

            BombDead?.Invoke(bombBlast);
        }
    }

    void SetGameOver(BiteType biteType)
    {
        gameOverCondition = true;
    }

    IEnumerator RemoveSmoke()
    {
        yield return new WaitForSeconds(2f);
        smokeObject.SetActive(false);
    }
}
