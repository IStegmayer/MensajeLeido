using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform eyesTransform;
    
    
    [SerializeField] private float speed;
    [SerializeField] private float smoothTime;
    // [SerializeField] private float smoothTime;
    // [SerializeField] private float smoothTime;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    private float xRotation;
    private float yRotation;
    private float mouseX;
    private float mouseY;
    private float currentVelocity;
    private CharacterController charController;
    private Vector2 moveInput;
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start() {
        charController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        HandleMovement();
        HandleRotation();
        HandleGravity();
    }

    private void HandleGravity() {
        float gravity = charController.isGrounded ? -0.01f : -9.8f;
        Vector3 fallVelocity = new Vector3(0.0f, gravity, 0.0f);
        
        charController.Move(fallVelocity * Time.deltaTime);
    }
    
    void HandleRotation() {
        yRotation += mouseX * Time.deltaTime * sensX;
        
        xRotation -= mouseY * Time.deltaTime * sensY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //smooth this out
        eyesTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
    }

    void HandleMovement() {
        
        if (moveInput.sqrMagnitude == 0) return;
        
        direction = moveInput.x * eyesTransform.right + moveInput.y * eyesTransform.forward;
        direction.y = 0.0f;
        
        charController.Move(direction *  (speed * Time.deltaTime));
    }

    public void Move(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }
    
    public void Camera(InputAction.CallbackContext context) {
        Vector2 cameraInput = context.ReadValue<Vector2>();
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }
}
