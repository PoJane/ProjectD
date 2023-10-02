using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // ����
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
    
    public Transform CurrentPlayer;     // ��ǰ1�����
    public Transform SecondPlayer;      // ��ǰ2�����    

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
