using UnityEngine;

public class MouseLook : MonoBehaviour
{
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
        float rotationX = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        float rotationY = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        Vector3 lookRotation = transform.eulerAngles + new Vector3(-rotationX, rotationY, 0);
        lookRotation.x = lookRotation.x > 180 ? lookRotation.x - 360 : lookRotation.x;
        lookRotation.x = Mathf.Clamp(lookRotation.x, -maxVerticalAngle, maxVerticalAngle);
        transform.eulerAngles = lookRotation;
    }
}
