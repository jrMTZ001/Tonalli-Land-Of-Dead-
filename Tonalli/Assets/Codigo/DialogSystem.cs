using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogueBox; // El panel donde se muestra el texto del di�logo
    public Text dialogueText; // El texto que se mostrar�
    public Button nextButton; // El bot�n para avanzar en el di�logo
    public string[] dialogueLines; // Las l�neas del di�logo

    private int currentLine = 0; // L�nea actual del di�logo
    private bool isPlayerInRange = false; // Controlar si el jugador est� cerca del NPC

    void Start()
    {
        // Al principio, la caja de di�logo est� oculta
        dialogueBox.SetActive(false);

        // Asociamos el evento del bot�n de avanzar
        nextButton.onClick.AddListener(ShowNextLine);
    }

    void Update()
    {
        // Detectar si el jugador est� cerca del NPC para iniciar el di�logo (esto se puede hacer con un collider)
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Usa la tecla E o la que prefieras
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        // Muestra la caja de di�logo al comenzar
        dialogueBox.SetActive(true);
        ShowNextLine(); // Muestra la primera l�nea
    }

    void ShowNextLine()
    {
        // Si a�n hay m�s l�neas de di�logo, muestra la siguiente
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
            currentLine++;
        }
        else
        {
            EndDialogue(); // Si ya no hay m�s l�neas, termina el di�logo
        }
    }

    void EndDialogue()
    {
        // Oculta la caja de di�logo al terminar
        dialogueBox.SetActive(false);
        currentLine = 0; // Reinicia el �ndice de l�neas para el siguiente di�logo
    }

    // M�todo para controlar cuando el jugador entra en el rango del NPC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Aseg�rate de que el jugador tenga el tag "Player"
        {
            isPlayerInRange = true;
        }
    }

    // M�todo para controlar cuando el jugador sale del rango del NPC
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

}
