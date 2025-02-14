using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public Transform jugador;  // Referencia al jugador
    public float suavizado = 0.1f;  // Suavidad del movimiento
    public Vector3 offset = new Vector3(0, 2, -10); // Posici�n relativa de la c�mara

    void LateUpdate()
    {
        if (jugador == null) return; // Evitar errores si no hay jugador asignado

        // Posici�n deseada de la c�mara (jugador + offset)
        Vector3 objetivoPos = jugador.position + offset;

        // Interpolaci�n suave entre la posici�n actual y la deseada
        transform.position = Vector3.Lerp(transform.position, objetivoPos, suavizado);
    }
}