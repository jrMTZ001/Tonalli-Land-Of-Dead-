using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        // Recupera la última posición guardada
        Vector2 position = PlayerPositionManager.GetSavedPosition();

        // Coloca al jugador en la posición guardada (si es diferente de Vector2.zero)
        if (position != Vector2.zero)
        {
            transform.position = position;
        }
    }
}

