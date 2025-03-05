using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLooteo : MonoBehaviour
{
    public GameObject coinPrefab;       // Prefab de la moneda
    public GameObject healthPrefab;     // Prefab del �tem de salud
    public float coinDropChance = 0.5f; // Probabilidad de que suelte una moneda
    public float healthDropChance = 0.3f; // Probabilidad de que suelte un �tem de salud

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        // Si el enemigo recibe suficiente da�o, muere
        // Esto es solo un ejemplo. Puede estar basado en el sistema de vida que tengas
        Die();
    }

    void Die()
    {
        anim.SetTrigger("Die");  // Activamos la animaci�n de muerte del enemigo

        // Dejamos un �tem con una probabilidad aleatoria
        Invoke("DropItem", 1f); // Llamamos a DropItem despu�s de 1 segundo

        // Desactivamos movimiento y collider
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        // Destruimos el objeto despu�s de un segundo
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
            Instantiate(healthPrefab, transform.position, Quaternion.identity); // Soltar �tem de salud
            Debug.Log("�tem de salud soltado");
        }
    }

}
