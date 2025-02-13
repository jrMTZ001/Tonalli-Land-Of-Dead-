using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public Transform jugador;  // Referencia al jugador
    public float suavizado = 0.2f;  // Suavizado del movimiento
    public Vector2 minLimite, maxLimite; // Límites de la cámara

    private Vector3 velocidad = Vector3.zero;

    void LateUpdate()
    {
        if (jugador == null) return;  // Evitar errores si el jugador no está asignado

        // Posición deseada de la cámara
        Vector3 objetivoPos = new Vector3(jugador.position.x, jugador.position.y, transform.position.z);

        // Aplicar límites
        objetivoPos.x = Mathf.Clamp(objetivoPos.x, minLimite.x, maxLimite.x);
        objetivoPos.y = Mathf.Clamp(objetivoPos.y, minLimite.y, maxLimite.y);

        // Movimiento suave hacia la posición deseada
        transform.position = Vector3.SmoothDamp(transform.position, objetivoPos, ref velocidad, suavizado);
    }
}
