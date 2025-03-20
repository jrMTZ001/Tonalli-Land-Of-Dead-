using UnityEngine;
using TMPro;
using System.Collections;

public class BlackHeartManager : MonoBehaviour
{
    public static BlackHeartManager Instance;

    public TextMeshProUGUI blackHeartText;
    private int heartsCollected = 0;
    private int heartsToUpgrade = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional si quieres mantenerlo en todas las escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectHeart()
    {
        heartsCollected++;
        ShowHeartMessage();

        if (heartsCollected >= heartsToUpgrade)
        {
            // Aquí va tu lógica de mejora de salud
            Debug.Log("¡Has ganado una mejora de salud!");
            // Reinicia contador si quieres coleccionar más
            heartsCollected = 0;
        }
    }

    private void ShowHeartMessage()
    {
        if (blackHeartText != null)
        {
            blackHeartText.text = $"¡Has obtenido {heartsCollected} de {heartsToUpgrade} corazones negros!";
            blackHeartText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterSeconds(2f)); // Muestra por 2 segundos
        }
    }

    private IEnumerator HideTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        blackHeartText.gameObject.SetActive(false);
    }
}

