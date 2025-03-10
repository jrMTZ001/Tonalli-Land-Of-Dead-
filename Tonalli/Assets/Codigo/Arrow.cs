using System.Data.Common;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la flecha
    public int damage = 10;  // Daño de la flecha
    public Rigidbody2D rb;   // Rigidbody de la flecha

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Agregar velocidad en la dirección de la flecha
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ajusta según el tag de tu jugador
        {
            other.GetComponent<PlayerHealthController>().DamagePlayer();
            Destroy(gameObject); // Destruir la flecha al impactar
        }
        else if (other.CompareTag("Ground")) // Si choca contra el suelo o una pared
        {
            Destroy(gameObject);
        }
    }
}
