using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Si el jugador toca el checkpoint
        {
            other.GetComponent<CaballeroJaguar>().SetCheckpoint(transform.position);
        }
    }
}
