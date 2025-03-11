using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    // Almacenar la �ltima posici�n conocida del jugador
    public static Vector2 savedPosition;

    // Funci�n para guardar la posici�n
    public static void SavePlayerPosition(Vector2 position)
    {
        savedPosition = position;
    }

    // Funci�n para obtener la posici�n guardada
    public static Vector2 GetSavedPosition()
    {
        return savedPosition;
    }
}
