using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanzador : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Se destruye tras cierto tiempo
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Dañar al enemigo
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
