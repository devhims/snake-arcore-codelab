/**
 * @author Kleber Ribeiro da Silva
 * @email krsmga@gmail.com
 * @create date 2020-05-16 19:42:04
 * @modify date 2020-06-16 19:15:33
 * @github https://github.com/krsmga/Camera-Permission
 */

using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// For compile Android devices
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

// For compile iOS devices
#if UNITY_IOS
using UnityEngine.iOS;
#endif

/// <summary>
/// Class PermissionStates defines constants for the camera access authorization states.
/// </summary>
/// <remarks>
/// <param name="DEFAULT">Never requested authorization.</param>
/// <param name="NOT_AUTHORIZED">Authorization denied.</param>
/// <param name="AUTHORIZED">Authorization granted.</param>
/// </remarks>
public class PermissionStates
{
    public const int DEFAULT = 0;       
    public const int NOT_AUTHORIZED = 1;
    public const int AUTHORIZED = 2;   
}

/// <summary>
/// Class CameraPermission was developed to facilitate the implementation of the code that requests permission to use the camera on Android and iOS devices, in applications developed with Unity.
/// </summary>
public class CameraPermission : MonoBehaviour
{
    // Objects containing informative texts
    [SerializeField] private GameObject textNotAuthorizedIOS = default;
    [SerializeField] private GameObject textNotAuthorizedAndroid = default;

    //Events that can be used depending on authorization
    public UnityEvent onAuthorized = default;
    public UnityEvent onNotAuthorized = default;

    // Private variables
    private delegate void OnVariableChangeDelegate(int value);

    /// <summary>
    /// The OnVariableChange(int) method is executed when the value of the isAuthorized variable is changed.
    /// </summary>
    private event OnVariableChangeDelegate OnVariableChange;
    private int _isAuthorized;

    /// <summary>
    /// Get / Set method for isAuthorized to work as OnChange.
    /// If a different value is set it will execute OnVariableChange(int).
    /// </summary>
    /// <returns>
    /// (int) -> _isAuthorized
    /// </returns>
    private int isAuthorized
    {
        get 
        { 
            return _isAuthorized; 
        }
        set 
        {
            if (_isAuthorized == value)
            {
                return;
            }

            _isAuthorized = value;
            if (OnVariableChange != null)
            {
                OnVariableChange(_isAuthorized);
            }
        }
    }

    void Awake() 
    {
        isAuthorized = PermissionStates.DEFAULT;

        if (textNotAuthorizedIOS != null)
        {
            textNotAuthorizedIOS.SetActive(false);
        }

        if (textNotAuthorizedAndroid != null)
        {
            textNotAuthorizedAndroid.SetActive(false);
        }

        OnVariableChange += OnVariableExecute;
    }

    #if UNITY_ANDROID
        public void RequestPersmission()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Permission.RequestUserPermission(Permission.Camera);
            }

            InvokeRepeating("CheckIsAuthorized", 1f, 1f);
        }

        // Works through InvokeRepeating("CheckIsAuthorized", 2f, 1f) in Awake()
        void CheckIsAuthorized()
        {
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                isAuthorized = PermissionStates.AUTHORIZED;
            }
            else
            {
                isAuthorized = PermissionStates.NOT_AUTHORIZED;
            }
        }
    #endif

    #if UNITY_IOS
        IEnumerator Start()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                isAuthorized = PermissionStates.AUTHORIZED;
            }
            else
            {
                isAuthorized = PermissionStates.NOT_AUTHORIZED;
            }
        }
    #endif

    // Works through OnVariableChange += OnVariableExecute in Awake()
    private void OnVariableExecute(int value)
    {
        if (value == PermissionStates.AUTHORIZED)
        {
            onAuthorized.Invoke();
        }
        else if (value == PermissionStates.NOT_AUTHORIZED)
        {
            onNotAuthorized.Invoke();
        }

        #if UNITY_ANDROID
            if (textNotAuthorizedAndroid != null)
            {
                textNotAuthorizedAndroid.SetActive(value == PermissionStates.NOT_AUTHORIZED);
            }
        #endif

        #if UNITY_IOS
            if (textNotAuthorizedIOS != null)
            {
                textNotAuthorizedIOS.SetActive(value == PermissionStates.NOT_AUTHORIZED);
            }
        #endif
    }
}
