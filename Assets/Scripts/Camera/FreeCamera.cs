using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 自由摄像机
 */
public class FreeCamera : BaseCamera
{
    [Header("基本属性")]
    public float MoveSpeed;     // 线性插值移动速度
    public float MoveDelta;     // 线性插值移动系数
    public float SmoothTime;    // 阻尼插值平滑时间

    public float MinDistance;               // 距离目标点的最小距离    

    [Header("计算属性")]
    public Vector3 CurTarget = Vector3.zero;    // 当前目标点
    public Vector3 CurVelocity = Vector3.zero;  // 当前速度

    [Header("测试")]
    public Transform Destination;    
    

    private void Start()
    {
        // 重设目标点
        CurTarget = transform.position;
    }

    private void Update()
    {        
        

    }

    /**
     * 计算目标点
     */
    public void CaculateTarget()
    {
        
    }

    private void LateUpdate()
    {
        LerpMove();   
    }

    /**
     * 线性插值移动
     */
    public void LerpMove()
    {
        if(Vector3.Distance(transform.position, CurTarget) > MinDistance)
        {
            transform.position = Vector3.Lerp(transform.position, CurTarget, MoveSpeed * MoveDelta * Time.deltaTime);
        }
    }

    /**
     * 阻尼插值移动
     */
    public void SmoothMove()
    {
        if (Vector3.Distance(transform.position, CurTarget) > MinDistance)
        {
            transform.position = Vector3.SmoothDamp(transform.position, CurTarget, ref CurVelocity, SmoothTime);
        }
    }
}
