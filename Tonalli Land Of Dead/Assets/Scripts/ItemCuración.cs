using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCuración : MonoBehaviour
{
    public int cantidadCuración = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CaballeroJaguar player = collision.GetComponent<CaballeroJaguar>();
            if(player != null)
            {
                player.RecuperarSalud(cantidadCuración);
            }
            Destroy(gameObject);
        }

    }
}
