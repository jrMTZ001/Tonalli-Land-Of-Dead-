using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject healthItemPrefab; // Prefab del ítem de salud
    public GameObject soulItemPrefab;   // Prefab del ítem de alma

    public float dropChanceHealth = 0.5f;  // Probabilidad de que suelte un ítem de salud
    public float dropChanceSoul = 0.5f;    // Probabilidad de que suelte un ítem de alma

    private bool hasDroppedItem = false;   // Para evitar que se suelten items más de una vez

    // Método que será llamado cuando el objeto reciba un golpe cuerpo a cuerpo
    public void OnHit()
    {
        if (!hasDroppedItem) // Verificamos si ya se soltó un item para evitar duplicados
        {
            DropItem();  // Llamamos a la función para soltar el ítem
            Destroy(gameObject);  // Destruimos el objeto después de soltar el ítem
            hasDroppedItem = true;  // Aseguramos que solo suelte un ítem una vez
        }
    }

    // Método para soltar el ítem
    private void DropItem()
    {
        // Elegir aleatoriamente si se suelta un ítem de salud, alma o ninguno
        float randomValueHealth = Random.value;
        float randomValueSoul = Random.value;

        if (randomValueHealth < dropChanceHealth)
        {
            // Si la probabilidad es menor que el valor, soltamos el ítem de salud
            Instantiate(healthItemPrefab, transform.position, Quaternion.identity);
            Debug.Log("Ítem de salud soltado");
        }
        else if (randomValueSoul < dropChanceSoul)
        {
            // Si la probabilidad es menor que el valor, soltamos el ítem de alma
            Instantiate(soulItemPrefab, transform.position, Quaternion.identity);
            Debug.Log("Ítem de alma soltado");
        }
    }

    // Detectar cuando el objeto recibe un golpe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el jugador golpea el objeto
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHit(); // Llamamos a la función para soltar el ítem y destruir el objeto
        }
    }
}


