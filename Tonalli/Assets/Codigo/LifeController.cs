using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{   
    public static LifeController instance;
    private Jugador thePlayer;
    public float respawnDelay = 2f;
    public GameObject deathEffect, respawnEffect;
    public int currentLives = 3;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindFirstObjectByType<Jugador>();
        if (UIController.instance != null)
        {
            UIController.instance.UpdateLivesDisplay(currentLives);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Respawn()
    {
        //thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
        //PlayerHealthController.Instance.AddHealth(PlayerHealthController.Instance.maxHealth);
        thePlayer.gameObject.SetActive(false);
        currentLives--;
        if(currentLives > 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            currentLives = 0;
            StartCoroutine(GameOverCo());

        }
        if (UIController.instance != null)
        {
            UIController.instance.UpdateLivesDisplay(currentLives);
        }
        Instantiate(deathEffect, thePlayer.transform.position, deathEffect.transform.rotation);
    }
    public IEnumerator RespawnCo()
    {
        yield return  new WaitForSeconds(respawnDelay);
        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
        PlayerHealthController.Instance.AddHealth(PlayerHealthController.Instance.maxHealth);
        thePlayer.gameObject.SetActive(true);
        Instantiate(respawnEffect, thePlayer.transform.position, Quaternion.identity);
    }
    public IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(respawnDelay);
        if(UIController.instance != null)
        {
            UIController.instance.ShowGameOver();
        }
    }
}
