using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    public GameObject[] foodModels;

    DetectedPlane detectedPlane;
    GameObject foodInstance;

    void OnEnable()
    {
        SceneController.PlaneSelected += SetSelectedPlane;
    }

    public void SetSelectedPlane(DetectedPlane selectedPlane)
    {
        detectedPlane = selectedPlane;
        foodInstance?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedPlane == null || detectedPlane.TrackingState != TrackingState.Tracking)
        {
            return;
        }

        //while (detectedPlane.SubsumedBy != null)
        //{
        //    detectedPlane = detectedPlane.SubsumedBy;
        //}

        if (foodInstance == null || foodInstance.activeSelf == false)
        {
            SpawnFoodInstance();
            return;
        }
    } 

    void SpawnFoodInstance()
    { 
        List<Vector3> vertices = new List<Vector3>();
        detectedPlane.GetBoundaryPolygon(vertices);

        Vector3 pt = vertices[Random.Range(0, vertices.Count)];
        float dist = Random.Range(0.05f, 1f);
        Vector3 position = Vector3.Lerp(pt, detectedPlane.CenterPose.position, dist);

        foodInstance = foodModels[Random.Range(0, foodModels.Length)];
        foodInstance.SetActive(true);
        foodInstance.transform.localPosition = position;
    }
}