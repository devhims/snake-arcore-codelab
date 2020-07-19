using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ScoreBoardController : MonoBehaviour
{
    public Camera firstPersonCamera;
    private Anchor anchor;
    private DetectedPlane detectedPlane;
    private float yOffset;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        if (detectedPlane == null)
        {
            return;
        }

        while (detectedPlane.SubsumedBy != null)
        {
            detectedPlane = detectedPlane.SubsumedBy;
        }

        transform.LookAt(firstPersonCamera.transform);

        transform.position = new Vector3(transform.position.x,
                    detectedPlane.CenterPose.position.y + yOffset, transform.position.z);

    }

    public void SetSelectedPlane(DetectedPlane detectedPlane)
    {
        this.detectedPlane = detectedPlane;
        CreateAnchor();
    }

    void CreateAnchor()
    {
        Vector2 pos = new Vector2(Screen.width * .5f, Screen.height * .90f);
        Ray ray = firstPersonCamera.ScreenPointToRay(pos);
        Vector3 anchorPosition = ray.GetPoint(5f);

        if (anchor != null)
        {
            DestroyObject(anchor);
        }
        anchor = detectedPlane.CreateAnchor(
            new Pose(anchorPosition, Quaternion.identity));

        transform.position = anchorPosition;
        transform.SetParent(anchor.transform);

        yOffset = transform.position.y - detectedPlane.CenterPose.position.y;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }

    public void SetScore(int score)
    {
        if (this.score != score)
        {
            GetComponentInChildren<TextMesh>().text = "Score: " + score;
            this.score = score;
        }
    }
}
