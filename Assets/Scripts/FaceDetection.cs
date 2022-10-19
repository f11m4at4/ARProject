using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARSubsystems;
using Unity.Collections;

public class FaceDetection : MonoBehaviour
{
    public Text logText;
    public Text verticesNum;
    public Button btn_Plus;
    public Button btn_Minus;
    public Transform faceCube;

    int vNumber = 0;
    ARFaceManager faceManager;
    Vector3 testPos;

    //enum FaceElement
    //{
    //    Nose = 20,
    //    UpperLib = 70,
    //}

    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();

        faceManager.facesChanged += args =>
        {
            if (args.updated.Count > 0)
            {
                // 메시 데이터를 가져온다.
                MeshFilter mf = args.updated[0].GetComponent<MeshFilter>();

                logText.text = $"얼굴 정점의 총 개수: {mf.mesh.vertices.Length}";

                Vector3 point = mf.mesh.vertices[vNumber];
                point = args.updated[0].transform.TransformPoint(point);

                Vector3 normal = mf.mesh.normals[vNumber];
                normal = args.updated[0].transform.TransformDirection(normal);
                //(mf.mesh.vertices[50] - testPos).y < 0
                //testPos = mf.mesh.vertices[50];


                faceCube.position = point;
                faceCube.forward = normal;
            }
        };

        btn_Plus.onClick.AddListener(IncreaseNumber);
        btn_Minus.onClick.AddListener(DecreaseNumber);

        //ARCoreStyleDetection();
    }

    public void IncreaseNumber()
    {
        vNumber = (vNumber + 1) % 468;
        verticesNum.text = vNumber.ToString();
    }

    public void DecreaseNumber()
    {
        vNumber = (468 + vNumber - 1) % 468;
        verticesNum.text = vNumber.ToString();
    }

    void ARCoreStyleDetection()
    {
        ARCoreFaceSubsystem subSys = (ARCoreFaceSubsystem)faceManager.subsystem;

        foreach(ARFace face in faceManager.trackables)
        {

            NativeArray<ARCoreFaceRegionData> faceRegionData = new NativeArray<ARCoreFaceRegionData>();

            subSys.GetRegionPoses(face.trackableId, Allocator.Persistent, ref faceRegionData);

            //faceRegionData[2].pose.position
        }
        
    }

}
