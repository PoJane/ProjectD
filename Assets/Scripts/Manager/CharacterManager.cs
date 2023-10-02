using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // 单例
    private static CharacterManager instance;
    public static CharacterManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<CharacterManager>();
                if(instance == null)
                {
                    GameObject o = new GameObject();
                    instance = o.AddComponent<CharacterManager>();
                }                
            }
            return instance;
        }
    }
    
    public Transform CurrentPlayer;     // 当前1号玩家
    public Transform SecondPlayer;      // 当前2号玩家    

    private void Awake()
    {
        CurrentPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        
    }

    void Update()
    {
              
    }
}
