using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [Header("���")]
    public CharacterController _controller;

    [Header("��Ϊ����")]
    public bool isControllable = true;
    public bool isMovable = true;
    public bool isSprintable = true;
    public bool isJumpable = true;

    [Header("����")]
    public float MoveSpeed;
    public float SprintSpeed;
    public float JumpSpeed;
    public float RotateSpeed;
    public float Health = 100;

    [Header("��������")]    
    public Vector3 CurVelocity = Vector3.zero;  // ��ǰ�ٶ�
    public Vector3 CurDirection = Vector3.zero; // ��ǰ����


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        Move();
        Test();
    }

    public virtual void Move()
    {        
        // ��õ�ǰ����ķ���
        CurDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //float y = Camera.main.transform.rotation.eulerAngles.y;
        // ��õ�ǰ����ķ���
        float y = CameraManager.Instance.CurrentMainCamera.rotation.eulerAngles.y;
        // ʹ��ǰ��ɫ����Χ�����y����תy�ȣ�ʹ��ת��һ��
        CurDirection = Quaternion.Euler(0, y, 0) * CurDirection;        
        CurVelocity = CurDirection * MoveSpeed * Time.deltaTime;
        _controller.Move(CurVelocity);
        // ��ɫ�����ƶ�����ת��
        transform.forward = Vector3.RotateTowards(transform.forward, CurDirection, RotateSpeed * Time.deltaTime, 0);
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Health -= 10;
            EventCenter.Instance.EventTrigger("PlayerCheckHealth", Health);            
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

}
