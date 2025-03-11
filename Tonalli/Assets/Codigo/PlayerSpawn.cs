using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        // Recupera la �ltima posici�n guardada
        Vector2 position = PlayerPositionManager.GetSavedPosition();

        // Coloca al jugador en la posici�n guardada (si es diferente de Vector2.zero)
        if (position != Vector2.zero)
        {
            transform.position = position;
        }
    }
}

