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

    AudioSource audioSource;
    Anchor anchor;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneController.PlaneSelected += SetSelectedPlane;
        BombMotion.BombDead += PlayAudio;
    }

    private void OnDisable()
    {
        SceneController.PlaneSelected -= SetSelectedPlane;
        BombMotion.BombDead -= PlayAudio;
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

        while (detectedPlane.SubsumedBy != null)
        {
            detectedPlane = detectedPlane.SubsumedBy;
        }

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

        anchor = null;
        anchor = detectedPlane.CreateAnchor(new Pose(position, Quaternion.identity));

        foodInstance = foodModels[Random.Range(0, foodModels.Length)];
        foodInstance.transform.position = position;
        foodInstance.SetActive(true);

        foodInstance.transform.parent = null;
        foodInstance.transform.SetParent(anchor.transform);
    }

    void PlayAudio(AudioClip audioClip)
    {
        audioSource?.PlayOneShot(audioClip);
    }
}