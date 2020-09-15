using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Linq;

public class FoodController : MonoBehaviour
{
    public GameObject[] foodModels;
    public ARAnchorManager aRReferencePointManager;


    GameObject foodInstance;

    AudioSource audioSource;
    ARAnchor anchor;

    ARPlane detectedPlane;
    Mesh planeMesh;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        planeMesh = new Mesh();
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

    public void SetSelectedPlane(ARPlane selectedPlane)
    {
        detectedPlane = selectedPlane;
        foodInstance?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedPlane == null || detectedPlane.trackingState != TrackingState.Tracking)
        {
            return;
        }

        while (detectedPlane.subsumedBy != null)
        {
            detectedPlane = detectedPlane.subsumedBy;
        }

        if (foodInstance == null || foodInstance.activeSelf == false)
        {
            SpawnFoodInstance();
            return;
        }
    }


    void SpawnFoodInstance()
    {
        var boundary = detectedPlane.boundary;
        var planeSpaceVertex = boundary[Random.Range(0, boundary.Length)];
        var worldSpaceVertex = detectedPlane.transform.TransformPoint(new Vector3(planeSpaceVertex.x, 0, planeSpaceVertex.y));

        float dist = Random.Range(0.05f, 1f);
        Vector3 position = Vector3.Lerp(worldSpaceVertex, detectedPlane.center, dist);

        anchor = null;
        anchor = aRReferencePointManager.AddAnchor(new Pose(position, Quaternion.identity));

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