using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{  
    public static PlayerHealthController Instance;

    public int currentHealth, maxHealth;
    public float invencibilityLenght = 1f;
    private float invencibilityCounter;
    public SpriteRenderer thSR;
    public Color normalColor, fadeColor;
    private Jugador thePlayer;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {   
        thePlayer = GetComponent<Jugador>();
        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(invencibilityCounter > 0)
        {
            invencibilityCounter -= Time.deltaTime;
            if(invencibilityCounter <= 0)
            {
                thSR.color = normalColor;
            }
        }
#if UNITY_EDITOR    
        if(Input.GetKeyDown(KeyCode.H))
        {
            AddHealth(1);
        }
#endif
    }

    public void DamagePlayer()
    {
        if (invencibilityCounter <= 0)
        {
            //invencibilityCounter = invencibilityLenght;
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                LifeController.instance.Respawn();
            }
            else
            {
                invencibilityCounter = invencibilityLenght;
                thSR.color = fadeColor;
                thePlayer.Knockback();
            }

            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }
    }

    public void AddHealth(int amountAdd)
    {
        currentHealth += amountAdd;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;  
        }

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

}
