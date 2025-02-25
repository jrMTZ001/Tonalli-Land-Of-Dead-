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
    public int maxHealth = 100;
    public int currentHealth;
    private bool isDead = false;

    private Rigidbody2D rb;
    private Animator animator;
    private bool movingRight = true;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
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
        /*
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
        */
        if (isAttacking || animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt")) return; // No moverse en Hurt

        animator.SetBool("isMoving", true);
        rb.velocity = new Vector2(movingRight ? speed : -speed, rb.velocity.y);

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
        /*
        animator.SetTrigger("Death");
        rb.velocity = Vector2.zero; // Detener movimiento
        GetComponent<Collider2D>().enabled = false; // Desactivar colisiones
        this.enabled = false; // Desactivar script

        Destroy(gameObject, 2f); // Eliminar el enemigo después de la animación
        */
        isDead = true;
        animator.SetBool("isDead", true);
        rb.velocity = Vector2.zero; // Detener el movimiento
        rb.bodyType = RigidbodyType2D.Static; // Congelar al enemigo
        GetComponent<Collider2D>().enabled = false; // Desactivar colisión para evitar interferencias
        this.enabled = false; // Desactivar el script del enemigo
    }

    public void TakeDamage(int damage)
    {
        /*
        maxHealth -= damage;
        animator.SetTrigger("Hurt"); // Si tienes animación de daño

        if (maxHealth <= 0)
        {
            Die();
        }
        */
        /*
        if (isDead) return; // Si ya está muerto, no recibir más daño.

        currentHealth -= damage;
        animator.SetTrigger("Hurt"); // Activar animación de daño

        // Aplicar un pequeño retroceso al enemigo cuando recibe daño
        rb.velocity = new Vector2(-transform.localScale.x * 2, rb.velocity.y);

        // Si la vida llega a 0, morir
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(RecoverFromHurt()); // Permitir que vuelva a moverse después de un tiempo
        }
        */
        /*
        if (isDead) return; // Evita recibir daño si ya está muerto

        currentHealth -= damage;
        animator.SetTrigger("Hurt"); // Activa la animación de daño

        // Detener el movimiento mientras está en Hurt
        rb.velocity = Vector2.zero;
        isAttacking = true; // Evita moverse o atacar mientras está en Hurt

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(RecoverFromHurt()); // Volver a moverse después de Hurt
        }
        */
        if (isDead) return; // Evita recibir daño si ya está muerto

        currentHealth -= damage;
        animator.SetTrigger("Hurt"); // Activa animación de daño

        rb.velocity = Vector2.zero; // Detener movimiento en Hurt
        isAttacking = true; // Evita moverse o atacar mientras está en Hurt

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(RecoverFromHurt()); // Reanudar patrulla después de Hurt
        }
    }
    private IEnumerator RecoverFromHurt()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Esperar hasta que termine Hurt

        isAttacking = false; // Habilitar movimiento otra vez
        animator.SetBool("isMoving", true); // Reanudar animación de caminar
    }
}
