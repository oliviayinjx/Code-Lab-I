using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] float horizontalSpeed = 7f;

    public float jumpHeight = 10f;
    private bool grounded = true;
    private float wallJumpCoolDown;
    private float dirX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");

        if (wallJumpCoolDown > 0.2f)
        {
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
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        else if (onWall() && !isGrounded())
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

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0 , Vector2.down, 0.1f,groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer); // detect the direction of the player
        return raycastHit.collider != null;
    }
}
