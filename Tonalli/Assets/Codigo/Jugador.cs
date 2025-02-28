using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    private bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded == true)
            {
                Jump();
            }
        }


        if(theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        if(theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f,1f);
        }
        //Animations
        anim.SetFloat("Speed",Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", theRB.velocity.y);

    }

    void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }
}
