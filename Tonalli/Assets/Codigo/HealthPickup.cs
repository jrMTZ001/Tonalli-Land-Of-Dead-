using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int addToHealth;
    public GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (PlayerHealthController.Instance.currentHealth != PlayerHealthController.Instance.maxHealth)
            {
                PlayerHealthController.Instance.AddHealth(addToHealth);
                Destroy(gameObject);
                SFXManager.Instance.PlayItemPickup();
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }
        }
    }
}
