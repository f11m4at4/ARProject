using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LightDetection : MonoBehaviour
{
    public Light directional;

    ARCameraManager camManager;


    void Start()
    {
        camManager.frameReceived += OnCameraFrame;
    }

    void OnCameraFrame(ARCameraFrameEventArgs args)
    {
        directional.intensity = (float)args.lightEstimation.averageMainLightBrightness;
        directional.colorTemperature = (float)args.lightEstimation.averageColorTemperature;
        directional.transform.rotation = Quaternion.LookRotation((Vector3)args.lightEstimation.mainLightDirection, directional.transform.up);
    }
}
