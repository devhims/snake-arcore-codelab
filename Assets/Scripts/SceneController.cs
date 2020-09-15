using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class SceneController : MonoBehaviour
{
    public static event Action<ARPlane> PlaneSelected;
    public static bool playing = false;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private void OnEnable()
    {
        FoodConsumer.SelfAnnihilation += GameOver;
    }

    private void OnDisable()
    {
        FoodConsumer.SelfAnnihilation -= GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTouches();
    }

    void ProcessTouches()
    {
        if (Input.touchCount > 0)
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();

                if (raycastManager.Raycast(touch0.position, hits, TrackableType.PlaneWithinBounds) && !playing)
                {
                    playing = true;
                    Time.timeScale = 1;

                    foreach (var plane in planeManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }

                    planeManager.planePrefab.GetComponent<MeshRenderer>().enabled = false;
                    planeManager.planePrefab.GetComponent<ARPlaneMeshVisualizer>().enabled = false;

                    PlaneSelected?.Invoke(planeManager.GetPlane(hits[0].trackableId));
                }
            }
        }
    }

    void GameOver(BiteType biteType)
    {
        playing = false;
        Time.timeScale = 0;
    }

}
