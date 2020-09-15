using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LifecycleManager : MonoBehaviour
{
    [SerializeField] ARSession m_Session;

    IEnumerator Start()
    {
        if (ARSession.state == ARSessionState.None || ARSession.state == ARSessionState.CheckingAvailability)
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            m_Session.enabled = false;
            // Start some fallback experience for unsupported devices
            _ShowAndroidToastMessage("We're sorry, your device is unsupported with this application");
            Invoke("_DoQuit", 0.5f);
        }
        else
        {
            // Start the AR session
            m_Session.enabled = true;
            //EventBroker.FireShowHandUIEvent();
        }
    }

    void Update()
    {
        UpdateApplicationLifecycle();
    }

    void UpdateApplicationLifecycle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Screen.sleepTimeout = ARSession.state != ARSessionState.SessionTracking ? SleepTimeout.SystemSetting : SleepTimeout.NeverSleep;

        if (ARSession.state != ARSessionState.SessionTracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

    }

    void _DoQuit()
    {
        Application.Quit();
    }

    void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
