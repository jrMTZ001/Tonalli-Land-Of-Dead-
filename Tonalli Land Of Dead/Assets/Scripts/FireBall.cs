using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la bola de fuego
    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // Normaliza la dirección
        transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1); // Voltea la fireball si es necesario
    }
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

