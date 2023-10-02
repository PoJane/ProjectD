using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [Header("组件")]
    public CharacterController _controller;

    [Header("行为控制")]
    public bool isControllable = true;
    public bool isMovable = true;
    public bool isSprintable = true;
    public bool isJumpable = true;

    [Header("属性")]
    public float MoveSpeed;
    public float SprintSpeed;
    public float JumpSpeed;
    public float RotateSpeed;
    public float Health = 100;

    [Header("计算属性")]    
    public Vector3 CurVelocity = Vector3.zero;  // 当前速度
    public Vector3 CurDirection = Vector3.zero; // 当前方向


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
        // 获得当前输入的方向
        CurDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //float y = Camera.main.transform.rotation.eulerAngles.y;
        // 获得当前相机的方向
        float y = CameraManager.Instance.CurrentMainCamera.rotation.eulerAngles.y;
        // 使当前角色方向围绕相机y轴旋转y度，使得转向一致
        CurDirection = Quaternion.Euler(0, y, 0) * CurDirection;        
        CurVelocity = CurDirection * MoveSpeed * Time.deltaTime;
        _controller.Move(CurVelocity);
        // 角色根据移动方向转身
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
