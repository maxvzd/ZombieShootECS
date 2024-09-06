using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private float sensitivity;
    [SerializeField] private float maxVerticalAngle;
    
    private float _rotationY;
    private float _rotationX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _rotationY += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        _rotationX += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -maxVerticalAngle, maxVerticalAngle);

        mainCamera.transform.eulerAngles = new Vector3(-_rotationX, _rotationY);
        //mainCamera.
    }
}
