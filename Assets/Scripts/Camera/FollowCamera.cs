using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("组件")]
    public Camera _camera;

    [Header("相机跟随")]
    public float MoveSpeed;     // 线性插值移动速度
    public float MoveDelta;     // 线性插值移动系数

    [Header("相机旋转")]
    public float RotateSpeed;   // 旋转速度    

    [Header("相机距离")]
    public float MouseScrollSensitive;
    public float MaxDistance;
    public float MinDistance;

    [Header("计算属性")]    
    public Transform CurTarget;    // 当前目标
    public Vector3 CurVelocity = Vector3.zero;  // 当前速度
    public float Distance;
    public float DesiredDistance;

    public Vector3 Offset = Vector3.zero;       // 距离目标差值
    public float OffsetSpeed;                   // 滑动滚轮改变差值速度

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        CurTarget = CharacterManager.Instance.CurrentPlayer;        
    }
   
    private void LateUpdate()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        if (CurTarget == null) return;
        HandleMove();
        ScrollOffset();
        HandleRotate();
    }

    /**
     * 线性插值移动
     */
    public void HandleMove()
    {
        transform.position = Vector3.Lerp(transform.position, CurTarget.position + Offset, MoveSpeed * MoveDelta * Time.deltaTime);
        transform.LookAt(CurTarget);        
    }

    /**
     * 移动鼠标旋转相机
     */
    public void HandleRotate()
    {
        //Quaternion targetQuan = Quaternion.LookRotation(CurTarget - transform.position, Vector3.up);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetQuan, RotateSpeed * Time.deltaTime);
        transform.RotateAround(transform.position,Vector3.up, Input.mousePosition.x);
    }

    /**
     * 鼠标中键使相机靠近、远离目标
     */
    public void HandleScroll()
    {
        float deadZone = 0.02f;
        if(Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone)
        {
            DesiredDistance = Mathf.Clamp(Distance - (Input.GetAxis("Mouse ScrollWheel") * MouseScrollSensitive), MinDistance, MaxDistance);
        }
    }

    /**
     * 鼠标滚轮改变相机FieldOfView以靠近、远离目标
     */
    public void ScrollOffset()
    {
        float d = Input.mouseScrollDelta.y;
        _camera.fieldOfView += d * MouseScrollSensitive * Time.deltaTime;
    }

    public float ClampAngle(float angle, float min, float max)
    {
        while(angle < -360 || angle > 360)
        {
            if (angle < -360) angle += 360;
            if (angle > 360) angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(CurTarget.position + Offset, 0.01f);    
    }
}
