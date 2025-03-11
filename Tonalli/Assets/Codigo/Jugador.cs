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
    public float knockbackLenght, knockbackSpeed;
    private float knockbackCounter;
    private bool canFlip;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Time.timeScale > 0f)
        {   
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

                if (knockbackCounter <= 0)
                {
                    theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

                    if (Input.GetButtonDown("Jump"))
                    {
                        if (isGrounded == true)
                        {
                            Jump();
                        }
                    }
                    //Mathf.Abs(theRB.velocity.x) >= 0.01f)
                    //Mathf.Abs(theRB.velocity.x)

                    if (theRB.velocity.x > 0)
                    {
                        transform.localScale = Vector3.one;
                    }
                    if (theRB.velocity.x < 0)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                }
                else
                {
                    knockbackCounter -= Time.deltaTime;
                    theRB.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, theRB.velocity.y);
                }
                        
            //Animations
            anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("ySpeed", theRB.velocity.y);
        }
       

    }

    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }
    void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }

   
    public void Knockback()
    {
        theRB.velocity = new Vector2(0f, jumpForce * .5f);
        anim.SetTrigger("isKnockingback");
        knockbackCounter = knockbackLenght;
    }
}
