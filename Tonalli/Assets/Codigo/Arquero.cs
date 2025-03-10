using System.Collections;
using UnityEngine;

public class Arquero : MonoBehaviour
{
    public Transform player;
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float shootCooldown = 2f;
    public float detectionRange = 7f;
    public GameObject deathParticlesPrefab; // Prefab de partículas

    private bool canShoot = true;
    private Animator anim;

    // Rango de ataque para que el arquero deje de disparar cuando el jugador se aleje
    public float attackRange = 5f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Distancia entre el arquero y el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador está en el rango de detección
        if (distanceToPlayer <= detectionRange)
        {
            // Si el jugador está dentro del rango de ataque, dispara
            if (distanceToPlayer <= attackRange && canShoot)
            {
                StartCoroutine(ShootArrow());
            }
            else
            {
                // Si el jugador está fuera del rango de ataque pero dentro del rango de detección
                // Requiere estar en Idle
                //anim.SetBool("isAttacking", false);
            }
        }
        else
        {
            // Si el jugador está fuera del rango de detección, vuelve a Idle
            //anim.SetBool("isAttacking", false);
        }
    }

    IEnumerator ShootArrow()
    {
        canShoot = false;
        anim.SetTrigger("Attack");  // Aquí se activa el trigger de Attack

        yield return new WaitForSeconds(0.5f); // Sincronización con la animación

        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);

        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    public void Die()
    {
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity); // Generar partículas
        anim.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }
}
