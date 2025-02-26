using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    public Animator anim;
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballCooldown = 3f;
    private bool isDead = false;
    public GameObject[] lootItems; // Array con los �tems (moneda o salud)
    public Transform lootSpawnPoint; // Punto donde se genera el loot

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating(nameof(Attack), 2f, fireballCooldown); // Ataca cada 3 segundos
    }

    void Attack()
    {
        if (isDead) return;
        anim.SetTrigger("Attack");
        Invoke(nameof(ShootFireball), 0.5f); // Dispara despu�s de la animaci�n
    }

    void ShootFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Determinar direcci�n de disparo seg�n la rotaci�n del enemigo
        float fireballDirection = transform.localScale.x > 0 ? 1f : -1f;

        fireball.GetComponent<Fireball>().SetDirection(new Vector2(fireballDirection, 0));
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            isDead = true;
            DropLoot(); // Suelta �tem
            anim.SetTrigger("Death");
            StartCoroutine(DestroyAfterDeath());
           // Destroy(gameObject); // Se destruye al morir
        }
    }
    void DropLoot()
    {
        if (lootItems.Length > 0)
        {
            int randomIndex = Random.Range(0, lootItems.Length); // Selecciona un �tem aleatorio
            Instantiate(lootItems[randomIndex], lootSpawnPoint.position, Quaternion.identity);
        }
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(0.5f); // Espera a que termine la animaci�n
        DropLoot(); // Genera el loot si tiene
        Destroy(gameObject); // Elimina el enemigo
    }
}
