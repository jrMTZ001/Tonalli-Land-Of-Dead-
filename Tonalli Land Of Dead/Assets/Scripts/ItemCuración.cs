using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCuración : MonoBehaviour
{
    public int cantidadCuracion = 20; // Cantidad de vida que restaura

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Verifica si el jugador lo toca
        {
            CaballeroJaguar player = collision.GetComponent<CaballeroJaguar>();
            if (player != null)
            {
                player.RecuperarSalud(cantidadCuracion); // Llama la función para curar
            }

            Destroy(gameObject); // Destruye el ítem de curación
        }
    }
}
