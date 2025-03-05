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
        if (isDead) return; // Si ya está muerto, ignorar el daño

        currentHealth -= damage;
        anim.SetTrigger("isHurt"); // Activar animación de daño

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        if (isDead) return;

        isDead = true;
        anim.SetBool("isDead", true); // Activar animación de muerte

        // Desactivar colisión y otros componentes para evitar interacciones
        col.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Detener movimiento
        GetComponent<Rigidbody2D>().gravityScale = 0; // Evitar caídas raras

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

        Destroy(gameObject, 2f); // Destruir enemigo después de 2 segundos
       
    }
        


}