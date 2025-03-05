using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    
    public float speed = 2f;
    public int health = 3;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Patrol();
        DetectObstacles();
    }

    void Patrol()
    {
        float moveDirection = isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        anim.SetBool("isMoving", true);
    }

    void DetectObstacles()
    {
        // Lanza un rayo hacia adelante para detectar paredes
        RaycastHit2D wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * (isFacingRight ? 1 : -1), 0.2f, groundLayer);

        // Lanza un rayo hacia abajo para detectar si hay suelo
        RaycastHit2D groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, groundLayer);

        // Dibuja los rayos en la escena para depuración (solo en modo de edición)
        Debug.DrawRay(wallCheck.position, Vector2.right * (isFacingRight ? 0.2f : -0.2f), Color.red);
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.5f, Color.green);

        // Si toca una pared o no hay suelo, se voltea
        if (wallDetected.collider != null || groundDetected.collider == null)
        {
            Flip();
        }
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
        rb.velocity = Vector2.zero; // Detiene el movimiento
        rb.isKinematic = true; // Evita colisiones
        GetComponent<Collider2D>().enabled = false; // Desactiva el collider
        Destroy(gameObject, 1f); // Se destruye después de 1 segundo
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    
    
}

