using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("���")]
    public Camera _camera;

    [Header("�������")]
    public float MoveSpeed;     // ���Բ�ֵ�ƶ��ٶ�
    public float MoveDelta;     // ���Բ�ֵ�ƶ�ϵ��

    [Header("�����ת")]
    public float RotateSpeed;   // ��ת�ٶ�    

    [Header("�������")]
    public float MouseScrollSensitive;
    public float MaxDistance;
    public float MinDistance;

    [Header("��������")]    
    public Transform CurTarget;    // ��ǰĿ��
    public Vector3 CurVelocity = Vector3.zero;  // ��ǰ�ٶ�
    public float Distance;
    public float DesiredDistance;

    public Vector3 Offset = Vector3.zero;       // ����Ŀ���ֵ
    public float OffsetSpeed;                   // �������ָı��ֵ�ٶ�

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
     * ���Բ�ֵ�ƶ�
     */
    public void HandleMove()
    {
        transform.position = Vector3.Lerp(transform.position, CurTarget.position + Offset, MoveSpeed * MoveDelta * Time.deltaTime);
        transform.LookAt(CurTarget);        
    }

    /**
     * �ƶ������ת���
     */
    public void HandleRotate()
    {
        //Quaternion targetQuan = Quaternion.LookRotation(CurTarget - transform.position, Vector3.up);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetQuan, RotateSpeed * Time.deltaTime);
        transform.RotateAround(transform.position,Vector3.up, Input.mousePosition.x);
    }

    /**
     * ����м�ʹ���������Զ��Ŀ��
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
     * �����ָı����FieldOfView�Կ�����Զ��Ŀ��
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
