using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenOptions()
    {
        Debug.Log("Abrir menú de opciones.");
    }

    public void QuitGame()
    {
        Debug.Log("Salir del Juego...");
        Application.Quit();
    }
}
