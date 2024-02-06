using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class CameraManager : MonoBehaviour
{
    public GameObject FPSCamera;
    public GameObject TPSCamera;
    public static Action<bool> OnViewChange;

    void Start()
    {
        Cursor.visible = false;
        TPSCamera.SetActive(true);
        FPSCamera.SetActive(false);
    }

    void OnSwitch()
    {
        if (FPSCamera.activeSelf)
        {
            FPSCamera.SetActive(false);
            TPSCamera.SetActive(true);
        }
        else if (TPSCamera.activeSelf)
        {
            FPSCamera.SetActive(true);
            TPSCamera.SetActive(false);
        }

        // Notify view change
        OnViewChange?.Invoke(TPSCamera.activeSelf);
    }
}
