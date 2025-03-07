using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float vertical;
    private bool isClimbing;

    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private LayerMask ladderLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Obtiene el Animator
    }

    private void Update()
    {
        // Detecta si el personaje está en la escalera
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.1f, ladderLayer);
        if (hit.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                isClimbing = true;
                rb.gravityScale = 0f; // Desactiva la gravedad mientras sube
                anim.SetBool("isClimbing", true); // Activa la animación
            }
        }

        if (isClimbing)
        {
            vertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);

            if (Input.GetKeyDown(KeyCode.Space)) // Para saltar desde la escalera
            {
                isClimbing = false;
                rb.gravityScale = 1f;
                anim.SetBool("isClimbing", false); // Desactiva la animación
            }
        }

        // Si no se está moviendo en la escalera, detener la animación
        if (isClimbing && vertical == 0)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Escaleras"))
        {
            isClimbing = true;
            rb.gravityScale = 0f;
            anim.SetBool("isClimbing", true); // Activa la animación
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Escaleras"))
        {
            isClimbing = false;
            rb.gravityScale = 1f;
            anim.SetBool("isClimbing", false); // Desactiva la animación
        }
    }
}

