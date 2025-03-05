using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLooteo : MonoBehaviour
{
    public GameObject coinPrefab;       // Prefab de la moneda
    public GameObject healthPrefab;     // Prefab del ítem de salud
    public float coinDropChance = 0.5f; // Probabilidad de que suelte una moneda
    public float healthDropChance = 0.3f; // Probabilidad de que suelte un ítem de salud

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        // Si el enemigo recibe suficiente daño, muere
        // Esto es solo un ejemplo. Puede estar basado en el sistema de vida que tengas
        Die();
    }

    void Die()
    {
        anim.SetTrigger("Die");  // Activamos la animación de muerte del enemigo

        // Dejamos un ítem con una probabilidad aleatoria
        Invoke("DropItem", 1f); // Llamamos a DropItem después de 1 segundo

        // Desactivamos movimiento y collider
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        // Destruimos el objeto después de un segundo
        Destroy(gameObject, 2f);
    }

    public void DropItem()
    {
        float dropChance = Random.Range(0f, 1f);

        // Si el valor aleatorio es menor o igual a la probabilidad de moneda
        if (dropChance <= coinDropChance)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity); // Soltar moneda
            Debug.Log("Moneda soltada");
        }
        // Si el valor aleatorio es menor o igual a la probabilidad de salud
        else if (dropChance <= coinDropChance + healthDropChance)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity); // Soltar ítem de salud
            Debug.Log("Ítem de salud soltado");
        }
    }

}
