using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f,0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;


    //current camera pos
    float cameraPitch = 0;
    float velocityY = 0;

    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //hide cursor 
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateCameraLook();
        UpdateMovement();
    }

    void updateCameraLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //smooth movement
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        //apply inverse y value 
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        //limit in a range
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        //move camera up and down 
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        //move camera left and right with mouseX position
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //diagnal also move in same speed
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);



          
    }
}
