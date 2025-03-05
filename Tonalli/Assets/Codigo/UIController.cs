using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{   
    public static UIController instance;
    public Image[] heartIcons;
    public Sprite heartFull, heartEmpty;
    public GameObject gameOverScreen;
    public TMP_Text livesText;
    public GameObject pauseScreen;
    public string mainMenuScene;
    public Text textoMonedas;  // Referencia al componente de texto en UI

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOnPause();
        }
        textoMonedas.text = " " + MonedaManager.Instance.ObtenerMonedas();

    }

    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for(int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;
            /*
            if(health <= i)
            {
                heartIcons[i].enabled = false;
            }
            */
            if (health > i)
            {
                heartIcons[i].sprite = heartFull;
            }
            else
            {
                heartIcons[i].sprite = heartEmpty;
                if (maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }
            }
        }
    }
    public void UpdateLivesDisplay(int currentLives)
    {
        livesText.text = currentLives.ToString();
    }
    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void Restart()
    {
        //Debug.Log("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void PauseOnPause()
    {
        if(pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
