using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    public GameObject[] lootItems; // Array con los objetos que puede soltar (moneda, salud, etc.)
    public float dropChance = 0.5f; // Probabilidad de que suelte un item (50% por defecto)

    public void DropLoot()
    {
        if (lootItems.Length > 0 && Random.value < dropChance) // Probabilidad de soltar loot
        {
            int randomIndex = Random.Range(0, lootItems.Length); // Elige un item aleatorio
            Instantiate(lootItems[randomIndex], transform.position, Quaternion.identity); // Crea el item en la posición del enemigo
        }
    }
}

