using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //try unity new input system

public class Player : MonoBehaviour
{
    //the player input
    Vector2 rawInput;
    //the boudnry of the screen, upleft and bottom right corner
    Vector2 minBounds;
    Vector2 maxBounds;
    //padding distance from the boundry of the screen
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    //player moving speed
    [SerializeField]float moveSpeed = 5f;

    //get shooter script
    Shooter shooter;
    private void Awake()
    {
        //get shooter
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        //get the boundry of the camera at the start
        InitBounds();
    }
    private void Update()
    {
        //player movement function
        Move();
    }

    void InitBounds()
    {
        //get the position of camera
        Camera mainCamera = Camera.main;
        //get the boundry of the camera to the world position
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    // Update is called once per frame
    void Move()
    {
        //how far player will move every frame
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        //use clamp to restrict moving area to the camera range
        newPos.x = Mathf.Clamp(transform.position.x + delta.x,minBounds.x + paddingLeft,maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        //assign position of player 
        transform.position = newPos; 
    }

    void OnMove(InputValue value)
    {
        //debug purpose 
        rawInput = value.Get<Vector2>();
    }
    //player fire projectile 
    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
