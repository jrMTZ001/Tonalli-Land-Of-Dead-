using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Button nextButton;
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool isPlayerInRange = false;

    // 🔥 Referencia al movimiento del player
    private Jugador playerMovement;

    void Start()
    {
        dialogueBox.SetActive(false);
        nextButton.onClick.AddListener(ShowNextLine);

        // 🔥 Buscar automáticamente al Player y su script de movimiento
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<Jugador>();
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueBox.SetActive(true);

        // 🔥 Desactivar movimiento
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
            currentLine++;
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        currentLine = 0;

        // 🔥 Reactivar movimiento
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
