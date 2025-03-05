using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int maxHealth = 3;
    private int currentHealth;
    private bool isDead = false;

    private Animator anim;
    private Collider2D col;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Si ya est� muerto, ignorar el da�o

        currentHealth -= damage;
        anim.SetTrigger("isHurt"); // Activar animaci�n de da�o

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        if (isDead) return;

        isDead = true;
        anim.SetBool("isDead", true); // Activar animaci�n de muerte

        // Desactivar colisi�n y otros componentes para evitar interacciones
        col.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Detener movimiento
        GetComponent<Rigidbody2D>().gravityScale = 0; // Evitar ca�das raras

        // Desactivar el script de movimiento
        if (GetComponent<EnemyPatrol>() != null)
        {
            GetComponent<EnemyPatrol>().enabled = false;
        }

        // Opcional: Desactivar otros scripts si el enemigo tiene ataques o IA
        if (GetComponent<EnemyShooter>() != null)
        {
            GetComponent<EnemyShooter>().enabled = false;
        }

        Destroy(gameObject, 2f); // Destruir enemigo despu�s de 2 segundos
       
    }
        


}