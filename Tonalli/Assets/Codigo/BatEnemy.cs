using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private float rangoDeDeteccion;  // Rango de detección del jugador
    [SerializeField] private Transform player;        // Referencia al jugador
    private int numeroAleatorio;
    private SpriteRenderer thSR;
    private Animator anim;
    public GameObject deathParticlesPrefab;  // Prefab de partículas

    private void Start()
    {
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        thSR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // Aseguramos que el Animator está asignado
        Girar();
    }

    private void Update()
    {
        float distanciaAlJugador = Vector2.Distance(transform.position, player.position);

        if (distanciaAlJugador <= rangoDeDeteccion) // Si el jugador está cerca, sigue al jugador
        {
            SeguirAlJugador();
        }
        else // Si no está cerca, patrulla entre puntos
        {
            Patrullar();
        }
    }

    private void SeguirAlJugador()
    {
        anim.SetBool("isFlying", true); // Activar vuelo cuando sigue al jugador
        transform.position = Vector2.MoveTowards(transform.position, player.position, velocidadDeMovimiento * Time.deltaTime);

        if (transform.position.x < player.position.x)
        {
            thSR.flipX = true;
        }
        else
        {
            thSR.flipX = false;
        }
    }

    private void Patrullar()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadDeMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
            Girar();
            anim.SetBool("isFlying", true); // Mantener vuelo cuando patrulla
        }
        else
        {
            anim.SetBool("isFlying", false); // Detener vuelo cuando no se mueve
        }
    }

    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            thSR.flipX = true;
        }
        else
        {
            thSR.flipX = false;
        }
    }

    public void Die()
    {
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity); // Generar partículas
        anim.SetTrigger("Death"); // Activar animación de muerte
        Destroy(gameObject, 1f); // Destruir el objeto después de la animación
    }
}
