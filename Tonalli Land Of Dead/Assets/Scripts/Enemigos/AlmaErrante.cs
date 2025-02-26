using UnityEngine;

public class AlmaErrante : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck, wallCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer, wallLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool movingRight = true;
    private bool isDead = false;

    public int maxHealth = 1; // Muere de un solo golpe
    private int currentHealth;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject healthItemPrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(movingRight ? speed : -speed, rb.velocity.y);
        anim.SetBool("isWalking", true); // Activa la animación de caminar

        bool isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkRadius, groundLayer);
        bool isTouchingWall = Physics2D.Raycast(wallCheck.position, movingRight ? Vector2.right : Vector2.left, checkRadius, wallLayer);

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
   
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            isDead = true;

            // Reproducir animación de muerte si la tienes
            if (anim != null)
            {
                anim.SetTrigger("Die");
            }

            // Generar loot (moneda o item de salud)
            DropLoot();

            // Esperar un pequeño tiempo antes de destruirlo (opcional, para que la animación se vea)
            Destroy(gameObject, 0.5f);
        }
    }
    void DropLoot()
    {
        int randomDrop = Random.Range(0, 2); // 0 = Moneda, 1 = Item de salud

        if (randomDrop == 0)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(healthItemPrefab, transform.position, Quaternion.identity);
        }
    }
    void Die()
    {
        isDead = true;
        anim.SetTrigger("Die"); // Activar animación de muerte
        rb.velocity = Vector2.zero; // Detener movimiento
        GetComponent<Collider2D>().enabled = false; // Desactivar colisión
        Destroy(gameObject, 0.5f); // Destruir después de la animación
    }
}
