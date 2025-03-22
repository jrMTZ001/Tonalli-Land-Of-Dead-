using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    
    public float jumpForce = 10f;
    public bool canAirJump = false;
    private bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Animator anim; 
    public float knockbackLenght, knockbackSpeed;
    private float knockbackCounter;
    private bool canFlip;
    public bool canMove;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float wallSlideSpeed = 2f;
    public LayerMask wallLayer;
    private bool isTouchingWall;
    public Transform wallCheck;
    public float wallCheckDistance = 0.5f;
    public int playerCoins = 0;
    // Start is called before the first frame update
    void Start()
    {
        

        
    }
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
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            ShootProjectile();
        }
        // Detección de pared
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        isTouchingWall = Physics2D.Raycast(wallCheck.position, direction, wallCheckDistance, wallLayer);

        // Debug visual del Raycast
        Debug.DrawRay(wallCheck.position, direction * wallCheckDistance, Color.red);

        // Si está tocando la pared, no está en el suelo, y está cayendo
        if (isTouchingWall && !IsGrounded() && theRB.velocity.y < 0)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, -wallSlideSpeed);
        }

        // Salto desde la pared
        if (Input.GetKeyDown(KeyCode.Space) && isTouchingWall)
        {
            float horizontalForce = -transform.localScale.x * jumpForce;
            theRB.velocity = new Vector2(horizontalForce, jumpForce);
        }

    }
    
    void ShootProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbProj = proj.GetComponent<Rigidbody2D>();
        rbProj.velocity = transform.localScale.x * Vector2.right * projectileSpeed;
    }
   
    bool IsGrounded()
    {
        // Tu lógica de detección del suelo
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
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
        SFXManager.Instance.PlayJump();

    }

    

    public void Knockback()
    {
        theRB.velocity = new Vector2(0f, jumpForce * .5f);
        anim.SetTrigger("isKnockingback");
        knockbackCounter = knockbackLenght;
    }

   
}
