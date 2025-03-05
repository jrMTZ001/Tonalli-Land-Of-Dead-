using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tonalli : MonoBehaviour
{
    public int valor = 1;  // Valor de la moneda

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Sumar monedas al sistema de manejo de monedas
            MonedaManager.Instance.AgregarMonedas(valor);

            // Destruir la moneda al recogerla
            Destroy(gameObject);
        }
    }
}
