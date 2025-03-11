using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    // Almacenar la última posición conocida del jugador
    public static Vector2 savedPosition;

    // Función para guardar la posición
    public static void SavePlayerPosition(Vector2 position)
    {
        savedPosition = position;
    }

    // Función para obtener la posición guardada
    public static Vector2 GetSavedPosition()
    {
        return savedPosition;
    }
}
