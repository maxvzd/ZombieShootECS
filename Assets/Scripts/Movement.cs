using UnityEngine;

public class Movement : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField] private float walkSpeed;
    [SerializeField] private Camera mainCamera; 
    
    // Start is called before the first frame update
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        float inputX = Input.GetAxis(Constants.VerticalKey) * Time.deltaTime * walkSpeed;
        float inputY = Input.GetAxis(Constants.HorizontalKey) * Time.deltaTime * walkSpeed;

        var cameraTransform = mainCamera.transform;
        Vector3 desiredDirection = inputX * cameraTransform.forward + inputY * cameraTransform.right;
        desiredDirection.y = 0;

        _controller.Move(desiredDirection);
    }
}
