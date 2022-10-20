using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;


public class ImageDetection : MonoBehaviour
{
    public Text logText;

    //public Dictionary<string, GameObject> imagePrefabs;
    public GameObject[] imagePrefabs;


    ARTrackedImageManager imageManager;



    void Start()
    {
        imageManager = GetComponent<ARTrackedImageManager>();


        imageManager.trackedImagesChanged += args =>
        {
            // �̹����� ó�� �ν����� ��
            if(args.added.Count > 0)
            {
                foreach(ARTrackedImage trackedImage in args.added)
                {
                    //GameObject test = Resources.Load<GameObject>($"./{trackedImage.referenceImage.name}");
                    //GameObject test2 = Resources.Load<GameObject>($"./{trackedImage.referenceImage.name}.prefab");
                    //GameObject go = Instantiate(Resources.Load<GameObject>($"./{trackedImage.referenceImage.name}"));

                    //imagePrefabs.Add(trackedImage.referenceImage.name, go);

                    GameObject generatePrefab = new GameObject();

                    foreach(GameObject prefab in imagePrefabs)
                    {
                        if(prefab.name ==trackedImage.referenceImage.name)
                        {
                            generatePrefab = prefab;
                        }
                    }

                    GameObject go = Instantiate(generatePrefab);

                    go.transform.position = trackedImage.transform.position;
                    go.transform.rotation = trackedImage.transform.rotation;
                    go.transform.localScale = trackedImage.transform.localScale;

                    if(go != null)
                    {
                        logText.text += go.name +"\r\n";
                    }
                    else
                    {
                        logText.text += "�ε� ����! \r\n";
                    }

                    //if(test != null)
                    //{
                    //    logText.text += "test1 ���ҽ� �ε�����! \r\n";
                    //}
                    //else
                    //{
                    //    logText.text += "test1 ���ҽ� �ε� ����!";
                    //}

                    //if (test2 != null)
                    //{
                    //    logText.text += "test2 ���ҽ� �ε�����! \r\n";
                    //}
                    //else
                    //{
                    //    logText.text += "test2 ���ҽ� �ε� ����!";
                    //}

                }
            }
            // �̹����� �ν� ���� ��...
            else if(args.updated.Count > 0)
            {
                foreach(ARTrackedImage trackedImage in args.updated)
                {
                    GameObject generatePrefab = new GameObject();

                    foreach (GameObject prefab in imagePrefabs)
                    {
                        if (prefab.name == trackedImage.referenceImage.name)
                        {
                            generatePrefab = prefab;
                        }
                    }

                    GameObject go = Instantiate(generatePrefab);

                    go.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
                    go.transform.localScale = trackedImage.transform.localScale;
                }
            }
        };
    }


    void Update()
    {
        
    }
}
