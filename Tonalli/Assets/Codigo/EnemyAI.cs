using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 3f;
    public int health = 3;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform playerCheck;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    private bool isFacingRight = true;
    private bool isPlayerNear = false;
    private bool isAttacking = false; // Nueva variable para controlar el ataque
    private Rigidbody2D rb;
    private Animator anim;

    public float attackDuration = 1f; // Duración del ataque

    private float attackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();
        DetectObstacles();

        if (!isPlayerNear && !isAttacking) // El enemigo se mueve solo si no está atacando ni cerca del jugador
        {
            Move();
        }

        if (isAttacking && Time.time >= attackTime + attackDuration)
        {
            StopAttack();
        }
    }

    void Move()
    {
        float moveDirection = isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        anim.SetBool("isMoving", true);
    }

    void DetectObstacles()
    {
        bool wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * (isFacingRight ? 1 : -1), 0.2f, groundLayer);
        bool groundMissing = !Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, groundLayer);

        if (wallDetected || groundMissing)
        {
            Flip();
        }
    }

    void DetectPlayer()
    {
        isPlayerNear = Physics2D.OverlapCircle(playerCheck.position, detectionRange, playerLayer);

        if (isPlayerNear && !isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", true);
        attackTime = Time.time; // Registra el tiempo de inicio del ataque
        Debug.Log("¡Atacando al jugador!");
        // Aquí iría la lógica de ataque (por ejemplo, detectar si golpea al jugador)
    }

    void StopAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", false); // Detener la animación de ataque
        Debug.Log("Ataque finalizado");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("isHurt");
        Debug.Log("Enemigo recibió daño: " + damage);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("isDead", true);
        Debug.Log("¡Enemigo eliminado!");
        Destroy(gameObject, 1f); // Espera 1 segundo antes de destruirlo
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
