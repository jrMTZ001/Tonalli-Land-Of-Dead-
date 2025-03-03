using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la bola de fuego

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject); // Se destruye al tocar el suelo
        }
    }
}

