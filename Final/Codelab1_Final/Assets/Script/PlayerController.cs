using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    //get player camera
    //[SerializeField] Transform playerCamera = null;
    //move speed
    [SerializeField] float mouseSensitivity = 3.5f;
    //not show cursor
    [SerializeField] bool lockCursor = true;
    [SerializeField] bool hideCursor = false; 
    //moving speed
    [SerializeField] float walkSpeed = 6.0f;
    //gavity value
    [SerializeField] float gravity = -13.0f;
    //jump
    [SerializeField] float jumpHeight = 3f;
    //movement smooth out
    [SerializeField] [Range(0.0f,0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;


    //current camera pos
    float cameraPitch = 0;
    float velocityY = 0;

    CharacterController controller = null;
    private GameObject _mainCamera;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    //
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //hide cursor 
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (hideCursor)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateCameraLook();
        UpdateMovement();

    }

    //player camera rotation
    void updateCameraLook()
    {
        //Don't multiply mouse input by Time.deltaTime
        //float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //smooth movement
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        //apply inverse y value 
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        //limit in a range
        cameraPitch = Mathf.Clamp(cameraPitch, -45.0f, 45.0f);

        //move camera up and down 
        // Update Cinemachine camera target pitch
        _mainCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0.0f, 0.0f);


        //move camera left and right with mouseX position
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        //grounded movement
        if (controller.isGrounded)
        {
            Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //diagnal also move in same speed
            targetDir.Normalize();

            currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

            //jump
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            //decreasing as dropping
            velocityY += gravity * Time.deltaTime;
        }

        //velocity 
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);     
    }

    void Jump()
    {
        velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
