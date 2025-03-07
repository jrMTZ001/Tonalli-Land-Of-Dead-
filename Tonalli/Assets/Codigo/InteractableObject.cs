using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject healthItemPrefab; // Prefab del �tem de salud
    public GameObject soulItemPrefab;   // Prefab del �tem de alma

    public float dropChanceHealth = 0.5f;  // Probabilidad de que suelte un �tem de salud
    public float dropChanceSoul = 0.5f;    // Probabilidad de que suelte un �tem de alma

    private bool hasDroppedItem = false;   // Para evitar que se suelten items m�s de una vez

    // M�todo que ser� llamado cuando el objeto reciba un golpe cuerpo a cuerpo
    public void OnHit()
    {
        if (!hasDroppedItem) // Verificamos si ya se solt� un item para evitar duplicados
        {
            DropItem();  // Llamamos a la funci�n para soltar el �tem
            Destroy(gameObject);  // Destruimos el objeto despu�s de soltar el �tem
            hasDroppedItem = true;  // Aseguramos que solo suelte un �tem una vez
        }
    }

    // M�todo para soltar el �tem
    private void DropItem()
    {
        // Elegir aleatoriamente si se suelta un �tem de salud, alma o ninguno
        float randomValueHealth = Random.value;
        float randomValueSoul = Random.value;

        if (randomValueHealth < dropChanceHealth)
        {
            // Si la probabilidad es menor que el valor, soltamos el �tem de salud
            Instantiate(healthItemPrefab, transform.position, Quaternion.identity);
            Debug.Log("�tem de salud soltado");
        }
        else if (randomValueSoul < dropChanceSoul)
        {
            // Si la probabilidad es menor que el valor, soltamos el �tem de alma
            Instantiate(soulItemPrefab, transform.position, Quaternion.identity);
            Debug.Log("�tem de alma soltado");
        }
    }

    // Detectar cuando el objeto recibe un golpe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el jugador golpea el objeto
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHit(); // Llamamos a la funci�n para soltar el �tem y destruir el objeto
        }
    }
}


