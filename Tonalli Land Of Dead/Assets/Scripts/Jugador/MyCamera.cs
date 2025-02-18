using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    
    public Transform jugador;  // Referencia al jugador
    //public float suavizado = 0.1f;  // Suavidad del movimiento
    //public Vector3 offset = new Vector3(0, 2, -10); // Posición relativa de la cámara
    public bool freezeVertical;
    public bool freezeHorizontal;
    public Vector3 positionStore;
    public bool clampPosition;
    public Transform clampMin, clampMax;
    private float halfWidth, halfHeight;
    public Camera theCam;

    void Start()
    {
        positionStore = transform.position;
        clampMin.SetParent(null);
        clampMax.SetParent(null);

        halfHeight = theCam.orthographicSize;
        halfWidth = theCam.orthographicSize * theCam.aspect;
    }

    void LateUpdate()
    {
        //if (jugador == null) return; // Evitar errores si no hay jugador asignado

        // Posición deseada de la cámara (jugador + offset)
        //Vector3 objetivoPos = jugador.position + offset;

        // Interpolación suave entre la posición actual y la deseada
        //transform.position = Vector3.Lerp(transform.position, objetivoPos, suavizado);
        transform.position = new Vector3(jugador.position.x, jugador.position.y, transform.position.z);
        if(freezeVertical == true)
        {
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);
        }
        if (freezeHorizontal == true)
        {
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);
        }
        if(clampPosition == true)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfHeight),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfWidth),
                transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        if(clampPosition == true)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));

            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
        }
    }

}