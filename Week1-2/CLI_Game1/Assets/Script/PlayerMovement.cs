using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private GameManager gameManager; 

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] float horizontalSpeed = 7f;

    [SerializeField] Color color1;
    [SerializeField] Color color2;
    private SpriteRenderer spriteRenderer;

    public float jumpHeight = 10f;
    private bool grounded = true;
    private float wallJumpCoolDown;
    private float dirX;
    int randNum;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameManager.findInstance();

        randNum = Random.Range(0, 2);

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

        if (wallJumpCoolDown > 0.2f)
        {
            //on wall jump and on ground jump
            rb.velocity = new Vector2(dirX * horizontalSpeed, rb.velocity.y);

            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.gravityScale = 1;
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCoolDown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        //if players is grounded, able to jump 
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        else if (onWall() && !isGrounded()) //climb on the verticle wall
        {
            if (dirX == 0)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 7, 0);              //add a velocity to push player away from the wall
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z); //flip direction of player when jump off wall
            }
            else
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);             //add a velocity to push player away from the wall
            }
            wallJumpCoolDown = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //check if the platform color is same as player color
        Color myColor = GetComponent<SpriteRenderer>().color;
        Debug.Log("mycolor: " + myColor);
        Color otherColor = col.gameObject.GetComponent<SpriteRenderer>().color;
        Debug.Log("platform color: " + otherColor);

        if (myColor.r <= otherColor.r + 0.02 && myColor.r >= otherColor.r - 0.02)
        {
            //Debug.Log("color is same");
        }
        else
        {
            //if land on the wrong color platform, level restarts
            death();
        }

        if (col.gameObject.layer == 10)
        {
            nextLevel();
        }
    }

    private bool isGrounded()
    {
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

    private void death()
    {
        GameManager.reloadCurrent();
    }

    private void nextLevel()
    {
        GameManager.loadNextScene();
        GameManager.score += 1;
    }
}