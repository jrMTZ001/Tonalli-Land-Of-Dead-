using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destruir proyectil despu�s de un tiempo
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aqu� podr�as llamar a una funci�n de da�o en el jugador
            Debug.Log("Jugador recibi� da�o");
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject); // Destruye el proyectil al tocar el suelo
        }
    }
}



