using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class LocationData : MonoBehaviour
{
    public Text logText;

    float idleTime = 5.0f;

    float curLatitude = 0;
    float curLongitude = 0;
    float curAltitude = 0;


    void Start()
    {
        StartCoroutine(WaitingPermission());

    }

    void Update()
    {
        
    }

    IEnumerator WaitingPermission()
    {

        // ��ġ ���� �����Ϳ� ���� �㰡�� �޴´�.
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }

        }

        if (!Input.location.isEnabledByUser)
        {
            logText.text = "������ ��ġ ������ ���ּ���!";
            yield break;
        }

        // ��ġ ���� ������ �����Ѵ�.
        Input.location.Start();

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            logText.text = "��ġ ���� ���� ����!";
        }
        else if(Input.location.status == LocationServiceStatus.Stopped)
        {
            logText.text = "����ڰ� ��ġ ������ OFF ����";
        }

        float currentTime = Time.deltaTime;

        while(Input.location.status == LocationServiceStatus.Initializing && currentTime < idleTime)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime += 1.0f;
            logText.text = $"������ {(int)currentTime}��";
        }

        if (currentTime > idleTime)
        {
            logText.text = "���� �ð��� �ʰ��߽��ϴ�.";
            yield break;
        }

        curLatitude = Input.location.lastData.latitude;
        curLongitude = Input.location.lastData.longitude;
        curAltitude = Input.location.lastData.altitude;

        logText.text = $"���� : {curLatitude} \r\n�浵: {curLongitude} \r\n��:{curAltitude}";
    }

}
