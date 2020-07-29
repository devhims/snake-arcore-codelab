using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    private DetectedPlane detectedPlane;
    private GameObject foodInstance;
    private float foodAge;
    private readonly float maxAge = 10f;
    public GameObject[] foodModels;

    void OnEnable()
    {
        SceneController.SetSelectedPlane += SetSelectedPlane;
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedPlane == null)
        {
            return;
        }

        if (detectedPlane.TrackingState != TrackingState.Tracking)
        {
            return;
        }
        while (detectedPlane.SubsumedBy != null)
        {
            detectedPlane = detectedPlane.SubsumedBy;
        }
        if (foodInstance == null || foodInstance.activeSelf == false)
        {
            SpawnFoodInstance();
            return;
        }
        foodAge += Time.deltaTime;
        if (foodAge >= maxAge)
        {
            DestroyObject(foodInstance);
            foodInstance = null;
        }

    } 

    public void SetSelectedPlane(DetectedPlane selectedPlane)
    {
        detectedPlane = selectedPlane;
    }

    void SpawnFoodInstance()
    { 
        GameObject foodItem = foodModels[Random.Range(0, foodModels.Length)]; 
        List<Vector3> vertices = new List<Vector3>();
        detectedPlane.GetBoundaryPolygon(vertices);
        Vector3 pt = vertices[Random.Range(0, vertices.Count)];
        float dist = Random.Range(0.05f, 1f);
        Vector3 position = Vector3.Lerp(pt, detectedPlane.CenterPose.position, dist);

        foodInstance = Instantiate(foodItem, position, Quaternion.identity);

        foodAge = 0;

        foodInstance.AddComponent<FoodMotion>();
    }
}