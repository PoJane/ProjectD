using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // ����
    private static CameraManager instance;
    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CameraManager>();
                if (instance == null)
                {
                    GameObject o = new GameObject();
                    instance = o.AddComponent<CameraManager>();
                }
            }
            return instance;
        }
    }

    public Transform CurrentMainCamera;     // ��ǰ1�����


    private void Awake()
    {
        CurrentMainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
}
