using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 5f;
    public float fireRate = 1f;

    [Header("Detección del Jugador")]
    public Transform player;
    public float detectionRange = 5f;
    public LayerMask playerLayer;

    [Header("Estados del Enemigo")]
    private bool isShooting = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        // Verifica si el jugador está en rango
        Collider2D playerDetected = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (playerDetected && !isShooting)
        {
            StartCoroutine(ShootSequence());
        }
    }

    IEnumerator ShootSequence()
    {
        isShooting = true;
        animator.SetTrigger("Attack");

        // Dispara el primer proyectil
        Shoot();
        yield return new WaitForSeconds(0.3f); // Espera 0.3 segundos entre disparos
        Shoot(); // Segundo disparo

        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de volver a disparar
        isShooting = false;
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja el rango de detección en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

