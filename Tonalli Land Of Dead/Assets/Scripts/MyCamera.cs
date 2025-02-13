using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public Transform jugador;  // Referencia al jugador
    public float suavizado = 0.2f;  // Suavizado del movimiento
    public Vector2 minLimite, maxLimite; // L�mites de la c�mara

    private Vector3 velocidad = Vector3.zero;

    void LateUpdate()
    {
        if (jugador == null) return;  // Evitar errores si el jugador no est� asignado

        // Posici�n deseada de la c�mara
        Vector3 objetivoPos = new Vector3(jugador.position.x, jugador.position.y, transform.position.z);

        // Aplicar l�mites
        objetivoPos.x = Mathf.Clamp(objetivoPos.x, minLimite.x, maxLimite.x);
        objetivoPos.y = Mathf.Clamp(objetivoPos.y, minLimite.y, maxLimite.y);

        // Movimiento suave hacia la posici�n deseada
        transform.position = Vector3.SmoothDamp(transform.position, objetivoPos, ref velocidad, suavizado);
    }
}
