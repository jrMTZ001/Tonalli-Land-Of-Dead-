using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogueBox; // El panel donde se muestra el texto del diálogo
    public Text dialogueText; // El texto que se mostrará
    public Button nextButton; // El botón para avanzar en el diálogo
    public string[] dialogueLines; // Las líneas del diálogo

    private int currentLine = 0; // Línea actual del diálogo
    private bool isPlayerInRange = false; // Controlar si el jugador está cerca del NPC

    void Start()
    {
        // Al principio, la caja de diálogo está oculta
        dialogueBox.SetActive(false);

        // Asociamos el evento del botón de avanzar
        nextButton.onClick.AddListener(ShowNextLine);
    }

    void Update()
    {
        // Detectar si el jugador está cerca del NPC para iniciar el diálogo (esto se puede hacer con un collider)
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Usa la tecla E o la que prefieras
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        // Muestra la caja de diálogo al comenzar
        dialogueBox.SetActive(true);
        ShowNextLine(); // Muestra la primera línea
    }

    void ShowNextLine()
    {
        // Si aún hay más líneas de diálogo, muestra la siguiente
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
            currentLine++;
        }
        else
        {
            EndDialogue(); // Si ya no hay más líneas, termina el diálogo
        }
    }

    void EndDialogue()
    {
        // Oculta la caja de diálogo al terminar
        dialogueBox.SetActive(false);
        currentLine = 0; // Reinicia el índice de líneas para el siguiente diálogo
    }

    // Método para controlar cuando el jugador entra en el rango del NPC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            isPlayerInRange = true;
        }
    }

    // Método para controlar cuando el jugador sale del rango del NPC
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

}
