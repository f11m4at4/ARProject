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

        // 위치 정보 데이터에 대한 허가를 받는다.
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }

        }

        if (!Input.location.isEnabledByUser)
        {
            logText.text = "폰에서 위치 정보를 켜주세요!";
            yield break;
        }

        // 위치 정보 수신을 시작한다.
        Input.location.Start();

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            logText.text = "위치 정보 수신 실패!";
        }
        else if(Input.location.status == LocationServiceStatus.Stopped)
        {
            logText.text = "사용자가 위치 정보를 OFF 했음";
        }

        float currentTime = Time.deltaTime;

        while(Input.location.status == LocationServiceStatus.Initializing && currentTime < idleTime)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime += 1.0f;
            logText.text = $"수신중 {(int)currentTime}초";
        }

        if (currentTime > idleTime)
        {
            logText.text = "지연 시간을 초과했습니다.";
            yield break;
        }

        curLatitude = Input.location.lastData.latitude;
        curLongitude = Input.location.lastData.longitude;
        curAltitude = Input.location.lastData.altitude;

        logText.text = $"위도 : {curLatitude} \r\n경도: {curLongitude} \r\n고도:{curAltitude}";
    }

}
