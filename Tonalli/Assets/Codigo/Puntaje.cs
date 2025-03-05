using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntaje : MonoBehaviour
{
    public int valor = 1;  // Valor de la moneda
    public static int contadorMonedas = 0;  // Contador global de monedas

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Aumentar el contador de monedas
            contadorMonedas += valor;

            // Mostrar el contador en la consola (opcional)
            Debug.Log("Monedas: " + contadorMonedas);

            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}
