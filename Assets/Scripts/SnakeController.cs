using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SnakeController : MonoBehaviour
{
    public GameObject snakeHeadPrefab;
    public GameObject pointer;
    public float speed = 20f;

    ARPlane detectedPlane;
    GameObject snakeInstance;
    Rigidbody rb;
    float dist;

    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void OnEnable()
    {
        SceneController.PlaneSelected += SetPlane;
        pointer.SetActive(false);
    }

    private void OnDisable()
    {
        SceneController.PlaneSelected -= SetPlane;
    }

    public void SetPlane(ARPlane plane)
    {
        detectedPlane = plane;
        SpawnSnake();
    }

    void SpawnSnake()
    {
        if (snakeInstance != null)
        {
            Destroy(snakeInstance);
        }

        Vector3 pos = detectedPlane.center;

        // Not anchored, it is rigidbody that is influenced by the physics engine.
        snakeInstance = Instantiate(snakeHeadPrefab, pos, Quaternion.identity, transform);
        rb = snakeInstance.GetComponent<Rigidbody>();

        // Pass the head to the slithering component to make movement work.
        GetComponent<Slithering>().Head = snakeInstance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedPlane == null)
        {
            return;
        }

        while (detectedPlane.subsumedBy != null)
        {
            detectedPlane = detectedPlane.subsumedBy;
        }


        if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinBounds))
        {
            if (planeManager.GetPlane(hits[0].trackableId) == detectedPlane)
            {
                if (!pointer.activeSelf)
                {
                    pointer.SetActive(true);
                    pointer.transform.position = hits[0].pose.position;
                }

                //float snakePosY = snakeInstance.transform.localPosition.y;

                Vector3 pt = hits[0].pose.position;
                //Set the Y to the Y of the snakeInstance
                //pt.y = snakePosY;

                // Set the y position relative to the plane and attach the pointer to the plane
                Vector3 pos = pointer.transform.position;
                //pos.y = snakePosY;

                // Now lerp to the position                                         
                pointer.transform.position = Vector3.Lerp(pos, pt, Time.smoothDeltaTime * speed);
            }
        }

        dist = Vector3.Distance(pointer.transform.localPosition, snakeInstance.transform.localPosition) - 0.05f;

        if (dist < 0)
        {
            dist = 0;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null && pointer.activeSelf)
        {
            rb.transform.LookAt(pointer.transform.position);
            rb.velocity = snakeInstance.transform.localScale.x * snakeInstance.transform.forward * dist / .01f;
        }

    }

    public int GetLength()
    {
        return GetComponent<Slithering>().GetLength();
    }
}
