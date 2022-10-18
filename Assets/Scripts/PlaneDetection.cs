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
        // ���� ������� �����ϱ�
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();

        indicator.SetActive(false);
    }

    void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();
        
        // AR ����ĳ������ �ؼ� �ٴڿ� �ε����ٸ�... 
        if(raycastManager.Raycast(screenCenter, hitInfo, TrackableType.PlaneWithinPolygon))
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                // ��ġ�� �ߴٸ� �ε������͸� ��ġ��Ű�� Ȱ��ȭ�Ѵ�.
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
