using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ���������
 */
public class FreeCamera : BaseCamera
{
    [Header("��������")]
    public float MoveSpeed;     // ���Բ�ֵ�ƶ��ٶ�
    public float MoveDelta;     // ���Բ�ֵ�ƶ�ϵ��
    public float SmoothTime;    // �����ֵƽ��ʱ��

    public float MinDistance;               // ����Ŀ������С����    

    [Header("��������")]
    public Vector3 CurTarget = Vector3.zero;    // ��ǰĿ���
    public Vector3 CurVelocity = Vector3.zero;  // ��ǰ�ٶ�

    [Header("����")]
    public Transform Destination;    
    

    private void Start()
    {
        // ����Ŀ���
        CurTarget = transform.position;
    }

    private void Update()
    {        
        

    }

    /**
     * ����Ŀ���
     */
    public void CaculateTarget()
    {
        
    }

    private void LateUpdate()
    {
        LerpMove();   
    }

    /**
     * ���Բ�ֵ�ƶ�
     */
    public void LerpMove()
    {
        if(Vector3.Distance(transform.position, CurTarget) > MinDistance)
        {
            transform.position = Vector3.Lerp(transform.position, CurTarget, MoveSpeed * MoveDelta * Time.deltaTime);
        }
    }

    /**
     * �����ֵ�ƶ�
     */
    public void SmoothMove()
    {
        if (Vector3.Distance(transform.position, CurTarget) > MinDistance)
        {
            transform.position = Vector3.SmoothDamp(transform.position, CurTarget, ref CurVelocity, SmoothTime);
        }
    }
}
