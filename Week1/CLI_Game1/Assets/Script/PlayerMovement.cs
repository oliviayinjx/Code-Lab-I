using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 40f;
    public float jumpHeight = 10f;
    [SerializeField]float horizontalSpeed = 7f;
    private bool grounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * horizontalSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        grounded = false; 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

/*    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast;
        return false;
    }*/
}
