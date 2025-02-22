using System.Collections;
using UnityEngine;

public class EnemyCalavera : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck;
    public Transform wallCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform player;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool movingRight = true;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= attackRange)
        {
            StartAttack();
        }
        else
        {
            if (isAttacking)
            {
                EndAttack(); // Detiene la animación de ataque si el jugador se aleja
            }
            Move();
        }
    }

    void Move()
    {
        if (isAttacking) return;  // No moverse si está atacando

        animator.SetBool("isMoving", true);
        rb.velocity = new Vector2(movingRight ? speed : -speed, rb.velocity.y);

        // Verifica si hay pared o borde
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        bool isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        if (!isGrounded || isTouchingWall)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.Rotate(0f, 180f, 0f);
        
    }

    void StartAttack()
    {
        // Voltear hacia el jugador antes de atacar
        if ((player.position.x > transform.position.x && !movingRight) ||
            (player.position.x < transform.position.x && movingRight))
        {
            Flip();
        }

        isAttacking = true;
        animator.SetBool("isAttacking", true);
        animator.SetBool("isMoving", false);
        rb.velocity = Vector2.zero;  // Detener movimiento
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isMoving", true);
    }
}
