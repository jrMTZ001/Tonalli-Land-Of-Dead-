using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CaballeroJaguar player = collision.GetComponent<CaballeroJaguar>();

            // Calcular dirección del knockback
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            player.TakeDamage(10, knockbackDirection);
        }
    }
}
