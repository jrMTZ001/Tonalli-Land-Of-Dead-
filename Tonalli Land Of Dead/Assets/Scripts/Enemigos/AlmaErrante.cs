using UnityEngine;

public class AlmaErrante : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck, wallCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    public int damageToPlayer = 10;

    private Rigidbody2D rb;
    private Animator anim;
    private bool movingRight = true;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        bool isGroundAhead = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        bool isWallAhead = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        if (!isGroundAhead || isWallAhead)
        {
            Flip();
        }

        rb.velocity = new Vector2(movingRight ? speed : -speed, rb.velocity.y);

        // **Actualiza la animación según la velocidad**
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CaballeroJaguar player = other.GetComponent<CaballeroJaguar>();
        if (player != null)
        {
            player.TakeDamage(damageToPlayer, transform.position);
        }
    }

    public void TakeDamage()
    {
        if (!isDead)
        {
            isDead = true;
            anim.SetTrigger("Die");  // **Activa la animación de muerte**
            Destroy(gameObject, 0.5f); // **Espera 0.5 segundos antes de destruirlo**
        }
    }
}
