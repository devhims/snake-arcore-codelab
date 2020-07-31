using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SnakeController : MonoBehaviour
{
    public GameObject snakeHeadPrefab;
    public GameObject pointer;
    public float speed = 20f;

    DetectedPlane detectedPlane;
    GameObject snakeInstance;
    Rigidbody rb;
    float dist;

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

    public void SetPlane(DetectedPlane plane)
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

        Vector3 pos = detectedPlane.CenterPose.position;

        // Not anchored, it is rigidbody that is influenced by the physics engine.
        snakeInstance = Instantiate(snakeHeadPrefab, pos, Quaternion.identity, transform);
        rb = snakeInstance.GetComponent<Rigidbody>();

        // Pass the head to the slithering component to make movement work.
        GetComponent<Slithering>().Head = snakeInstance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //while (detectedPlane.SubsumedBy != null)
        //{
        //    detectedPlane = detectedPlane.SubsumedBy;
        //}

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds;

        if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
        {
            if (hit.Trackable as DetectedPlane == detectedPlane)
            {
                pointer.SetActive(true);
                //float snakePosY = snakeInstance.transform.localPosition.y;

                Vector3 pt = hit.Pose.position;
                //Set the Y to the Y of the snakeInstance
                //pt.y = snakePosY;

                // Set the y position relative to the plane and attach the pointer to the plane
                Vector3 pos = pointer.transform.localPosition;
                //pos.y = snakePosY;

                // Now lerp to the position                                         
                pointer.transform.localPosition = Vector3.Lerp(pos, pt, Time.smoothDeltaTime * speed);
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
        rb.transform.LookAt(pointer.transform.position);
        rb.velocity = snakeInstance.transform.localScale.x * snakeInstance.transform.forward * dist / .01f;
    }

    public int GetLength()
    {
        return GetComponent<Slithering>().GetLength();
    }
}
