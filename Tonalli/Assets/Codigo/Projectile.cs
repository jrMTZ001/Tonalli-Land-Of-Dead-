using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destruir proyectil después de un tiempo
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí podrías llamar a una función de daño en el jugador
            Debug.Log("Jugador recibió daño");
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject); // Destruye el proyectil al tocar el suelo
        }
    }
}



