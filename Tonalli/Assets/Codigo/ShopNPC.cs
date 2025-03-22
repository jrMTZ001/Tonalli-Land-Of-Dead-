using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject dialogueUI; // UI Panel con el diálogo
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenDialogue();
        }
    }

    void OpenDialogue()
    {
        dialogueUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego mientras compras
    }

    public void CloseDialogue()
    {
        dialogueUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            // Mostrar indicador "Presiona E para hablar"
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            // Ocultar indicador
        }
    }
}
