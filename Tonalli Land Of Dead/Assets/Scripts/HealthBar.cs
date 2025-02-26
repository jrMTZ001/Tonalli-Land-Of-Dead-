using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthFill; // La imagen de la barra de vida

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthFill.fillAmount = currentHealth / maxHealth;
    }
}
