using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // 单例
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

    public Transform CurrentMainCamera;     // 当前1号玩家


    private void Awake()
    {
        CurrentMainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
}
