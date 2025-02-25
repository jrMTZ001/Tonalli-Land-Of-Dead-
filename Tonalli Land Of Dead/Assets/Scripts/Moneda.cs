using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valorMoneda = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CaballeroJaguar player = collision.GetComponent<CaballeroJaguar>();
            if(player != null)
            {
                player.AgregarPuntos(valorMoneda);
            }
            Destroy(gameObject);
        }
    }
}
