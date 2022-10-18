using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneDetection : MonoBehaviour
{
    public GameObject indicator;

    ARPlaneManager planeManager;
    ARRaycastManager raycastManager;

    void Start()
    {
        // 폰의 절전모드 방지하기
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();

        indicator.SetActive(false);
    }

    void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();
        
        // AR 레이캐스팅을 해서 바닥에 부딪혔다면... 
        if(raycastManager.Raycast(screenCenter, hitInfo, TrackableType.PlaneWithinPolygon))
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                // 터치를 했다면 인디케이터를 위치시키고 활성화한다.
                indicator.SetActive(true);
                indicator.transform.position = hitInfo[0].pose.position;
                indicator.transform.rotation = hitInfo[0].pose.rotation;
                //indicator.transform.rotation *= Quaternion.Euler(new Vector3(90, 0, 0));
            }
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    public void ToggleDebugging()
    {
        planeManager.enabled = !planeManager.enabled;

        foreach (ARPlane plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(planeManager.enabled);
        }
    }
}
