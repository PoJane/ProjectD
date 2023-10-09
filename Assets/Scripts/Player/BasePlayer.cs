using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [Header("组件")]
    public CharacterController _controller;
    public Animator _animator;

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

    // 检测是否落地
    public float Gravity;
    public bool isGround;
    public float CheckRadius;
    public LayerMask GroundMask;

    [Header("计算属性")]    
    [SerializeField]private Vector3 CurVelocity = Vector3.zero;  // 当前速度
    [SerializeField]private Vector3 CurDirection = Vector3.zero; // 当前方向


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
        CheckGround();
        Test();
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _animator.SetBool("isRun", true);
        }
        else
        {
            _animator.SetBool("isRun", false);
        }
    }

    public virtual void Move()
    {        
        // 获得当前输入的方向
        CurDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));             
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

    public void CheckGround()
    {
        isGround = Physics.CheckSphere(transform.position, CheckRadius, GroundMask);    
        if(isGround && CurVelocity.y < 0)
        {
            CurVelocity.y = -2;
        }
        CurVelocity.y -= Time.deltaTime * Gravity;
        _controller.Move(CurVelocity);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

}
