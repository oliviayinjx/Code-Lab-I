using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private GameManager gameManager; 

    //need to assign ground and wall layer for player
    //to detect whether it is collide with wall or ground
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    //player movement speed
    [SerializeField] float horizontalSpeed = 7f;

    //each level has two colors, assign colors for each level
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [HideInInspector]public SpriteRenderer spriteRenderer;

    //player jump height
    public float jumpHeight = 10f;
    //to detect whether player is on the ground or not
    private bool grounded = true;
    //cool down time before next wall jump
    private float wallJumpCoolDown;
    //determine the x direction
    private float dirX;
    //for generating a random number and assign one of two colors at the start of the level
    int randNum;

    //two states bool for animation
    [HideInInspector]public bool jumpThisFrame;
    [HideInInspector] public bool landed;
    [HideInInspector] public bool landThisFrame;
    
    //check in air
    float groundY;
    float jumpY;


    void Start()
    {
        //get the rigidbody and box collider of player
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        //get the sprite renderer to change the color
        spriteRenderer = GetComponent<SpriteRenderer>();
        //find the game manager
        gameManager = GameManager.findInstance();
        //randon num will generated between 0 and 1
        randNum = Random.Range(0, 2);

        //if number is 1, player will be generated with color 1 at the start of game
        if (randNum == 1)
        {
            spriteRenderer.color = color1;
            //Debug.Log("blueblue" + randNum);
        }
        else
        {
            spriteRenderer.color = color2;
            //Debug.Log("orangeorange" + randNum);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        //dirX is horintonal axis, a/d, left or right
        dirX = Input.GetAxis("Horizontal");

        //not allow to jump until 0.2s later
        if (wallJumpCoolDown > 0.2f)
        {
            //on wall jump and on ground jump
            rb.velocity = new Vector2(dirX * horizontalSpeed, rb.velocity.y);
            //if player on wall and not on ground
            //turn of gravity and velocity
            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else //if not on wall or not on ground, or only on ground, turn gravity to normal
            {
                rb.gravityScale = 1;
            }
            //when press space button, jump

             Jump();
            

        }
        else
        {
            wallJumpCoolDown += Time.deltaTime; //counting the cool down time
        }

        if (isGrounded() && rb.velocity.y > 0.1f)
        {
            landThisFrame = false;
            landed = false;
        }
  

        
    }
   
    private void Jump()
    {
        //if players is grounded, able to jump 
        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            //jump height is set at the front side
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpThisFrame = true;
        }
        else if (onWall() && !isGrounded() && Input.GetButtonDown("Jump")) //climb on the verticle wall
        {
            jumpThisFrame = true;
            if (dirX == 0)
            {
                //add a velocity to push player away from the wall
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 7, 0);              
                //flip direction of player when jump off wall
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z); 
            }
            else
            {
                //add a velocity to push player away from the wall
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);             
            }
            wallJumpCoolDown = 0; //clear cool down timer
        }
        else
        {
            jumpThisFrame = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //check if the platform color is same as player color
        Color myColor = GetComponent<SpriteRenderer>().color;
        //Debug.Log("mycolor: " + myColor);
        Color otherColor = col.gameObject.GetComponent<SpriteRenderer>().color;
        //Debug.Log("platform color: " + otherColor);

        if (myColor.r <= otherColor.r + 0.02 && myColor.r >= otherColor.r - 0.02)
        {
            //Debug.Log("color is same");
        }
        else
        {
            //if land on the wrong color platform, level restarts
            death();
        }
        //if player collide with the last platform ( in third color), they successfully pass this level
        if (col.gameObject.layer == 10)
        {
            nextLevel();
        }

        if (col.gameObject.layer == 8) //ground
        {
            landThisFrame = true;
        }
    }


    private bool isGrounded()
    {
        landed = true; 
        //if the raycast hit the box under the player and is at the ground layer
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        //if the raycast hit the box on the right or left side of the player (depending where it faces)
        //and is at the wall layer 
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer); // detect the direction of the player
        return raycastHit.collider != null;
    }

    //if player dies,restart this level
    private void death()
    {
        GameManager.reloadCurrent();
    }
    //load next level
    //level score plus 1
    private void nextLevel()
    {
        GameManager.loadNextScene();
        GameManager.score += 1;
    }

}