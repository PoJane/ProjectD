using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonCamera : BaseCamera
{
    [Header("现在跟随目标")]
    public Transform CurrentTarget;

    [Header("与目标的距离")]
    public float Distance = 25.0f;
    public float DistanceMin = 10.0f;
    public float DistanceMax = 50.0f;

    [Header("鼠标输入")]
    private float mouseX = 0.0f;
    private float mouseY = 0.0f;

    [Header("鼠标灵敏度")]
    public float X_MouseSensitivity = 5.0f;
    public float Y_MouseSensitivity = 5.0f;
    public float MouseWheelSensitivity = 5.0f;
    public float DistanceSmooth = 0.05f;
    public float X_Smooth = 0.05f;
    public float Y_Smooth = 0.1f;

    [Header("垂直视角限制")]
    public float Y_MinLimit = -40.0f;
    public float Y_MaxLimit = 80.0f;

    [Header("保持在水平面以上")]
    public bool keepAboveWater = true;
    public bool YHeight_Min = false;
    public float YHeight = 1f;


    // 计算距离
    private float startingDistance = 0.0f;
    private float desiredDistance = 0.0f;    
    private float velocityDistance = 0.0f;
    // 计算位置
    public Vector3 desiredPosition = Vector3.zero;
    public Vector3 position = Vector3.zero;

    private float velX = 0.0f;
    private float velY = 0.0f;
    private float velZ = 0.0f;           
    
    void Start()
    {
        //一开始不计算距离插值 避免一开始镜头运动
        //Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
        Distance = DistanceMin;
        startingDistance = Distance;
        Reset();
    }

    private void Update()
    {
        base.Update();
    }

    void LateUpdate()
    {
        base.LateUpdate();
        CameraUpdate();
    }

    void CameraUpdate()
    {
        if (CurrentTarget == null)
        {
            CurrentTarget = CharacterManager.Instance.CurrentPlayer;
            if(CurrentTarget == null)
            {
                return;
            }
        }            
        HandlePlayerInput();
        CalculateDesiredPosition();
        UpdatePosition();
    }

    /**
     * 获得鼠标x、y方向的输入
     * 限制y输入值
     */
    public void CaculateMouse()
    {
        mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;

        // this is where the mouseY is limited - Helper script
        mouseY = ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);        
    }

    /**
     * 处理鼠标以及鼠标中键输入
     * 限制最近、最远距离
     */
    public void HandlePlayerInput()
    {
        //if(EventSystem.)
        CaculateMouse();
        // Evaluate distance
        float deadZone = 0.02f;        

        // get Mouse Wheel Input
        if (Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone)
        {
            desiredDistance = Mathf.Clamp(Distance - (Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity),DistanceMin, DistanceMax);
        }
    }

    /**
     * 计算目标位置
     */
    public void CalculateDesiredPosition()
    {        
        Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velocityDistance, DistanceSmooth);

        // Calculate desired position -> Note : mouse inputs reversed to align to WorldSpace Axis
        desiredPosition = CalculatePosition(mouseY, mouseX, Distance);

        if (keepAboveWater && desiredPosition.y < 0.4f) desiredPosition.y = 0.4f;
    }

    /**
     * 计算目标距离、方向所在位置
     */
    public Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return YHeight_Min ? new Vector3(CurrentTarget.position.x, YHeight, CurrentTarget.position.z) + (rotation * direction) : CurrentTarget.position + (rotation * direction);
    }

    /**
     * 平滑更新transform位置
     */
    public void UpdatePosition()
    {
        var posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
        var posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth);
        var posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);
        position = new Vector3(posX, posY, posZ);

        transform.position = position;
        if(CurrentTarget.Find("Head") != null)
        {
            CurrentTarget = CurrentTarget.Find("Head");
        }
        transform.LookAt(CurrentTarget);        
    }

    // 重置
    public void Reset()
    {
        mouseX = 0;
        //mouseY = 10;
        mouseY = 0;
        Distance = startingDistance;
        desiredDistance = Distance;
    }

    /**
     * 角度校正
     */
    public float ClampAngle(float angle, float min, float max)
    {
        while (angle < -360 || angle > 360)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
}
