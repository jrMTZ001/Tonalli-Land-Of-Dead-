using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakeableObject : MonoBehaviour
{
    public int health = 1; // Puedes aumentar la vida si quieres que resista más golpes

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyWall();
        }
    }

    void DestroyWall()
    {
        Debug.Log("¡Pared destruida!");
        Destroy(gameObject);
    }
}
