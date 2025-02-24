using System.Collections;
using UnityEngine;

public class EnemyCalavera : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform attackPoint; // Nuevo: Punto de ataque
    public float checkRadius = 0.2f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;
    public LayerMask groundLayer;
    public LayerMask playerLayer; // Nuevo: Capa del jugador
    public Transform player;
    public int health =  50;

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
        /*
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
        */
        if (player.GetComponent<CaballeroJaguar>().isDead)
        {
            // Si el jugador está muerto, continuar patrullando
            isAttacking = false;
            animator.SetBool("isAttacking", false);
            Move();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            StartAttack();
        }
        else
        {
            if (isAttacking)
            {
                EndAttack();
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
        if ((player.position.x > transform.position.x && !movingRight) ||
            (player.position.x < transform.position.x && movingRight))
        {
            Flip();
        }

        isAttacking = true;
        animator.SetBool("isAttacking", true);
        animator.SetBool("isMoving", false);
        rb.velocity = Vector2.zero;
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isMoving", true);
    }

    // **Nuevo: Aplicar daño al jugador**
    public void ApplyDamage(int damage)
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (playerCollider != null)
        {
            playerCollider.GetComponent<CaballeroJaguar>().TakeDamage(20, transform.position);
        }
    }
    void Die()
    {
        animator.SetTrigger("Death");
        rb.velocity = Vector2.zero; // Detener movimiento
        GetComponent<Collider2D>().enabled = false; // Desactivar colisiones
        this.enabled = false; // Desactivar script

        Destroy(gameObject, 2f); // Eliminar el enemigo después de la animación
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt"); // Si tienes animación de daño

        if (health <= 0)
        {
            Die();
        }
    }
}
