using System;
using UnityEngine;

public class DeadZoneLook : MonoBehaviour
{
    [SerializeField] private Transform lookAtBase;
    [SerializeField] private Transform aimAtBase;

    [SerializeField] private float maxGunAimAngle;
    [SerializeField] private float sensitivity;
    [SerializeField] private float maxVerticalAngle;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float rotationX = Input.GetAxis("Mouse Y") * sensitivity;
        float rotationY = Input.GetAxis("Mouse X") * sensitivity;
        
        Vector3 aimRotation = aimAtBase.eulerAngles + new Vector3(-rotationX, rotationY, 0);
        Vector3 lookRotation = lookAtBase.eulerAngles;

        lookRotation += CalculateDeadZone(aimRotation.y, lookRotation.y, Vector3.up) * rotationY;
        lookRotation += CalculateDeadZone(aimRotation.x, lookRotation.x, Vector3.right) * -rotationX;

        aimRotation.x = ClampEulerAngle(aimRotation.x, maxVerticalAngle);
        lookRotation.x = ClampEulerAngle(lookRotation.x, maxVerticalAngle);

        lookAtBase.eulerAngles = lookRotation;
        aimAtBase.eulerAngles = aimRotation;
    }

    private Vector3 CalculateDeadZone(float aimRotation, float lookRotation, Vector3 direction)
    {
        Vector3 offset = Vector3.zero;
        if (Mathf.Abs(aimRotation - lookRotation) > maxGunAimAngle)
        {
            offset = direction;
        }
        return offset;
    }

    private static float ClampEulerAngle(float eulerAngleToClamp, float angleToClampTo)
    {
        eulerAngleToClamp = GetRealAngle(eulerAngleToClamp);
        return Mathf.Clamp(eulerAngleToClamp, -angleToClampTo, angleToClampTo);
    }

    private static float GetRealAngle(float angle)
    {
        return angle > 180 ? angle - 360 : angle;
    }
}