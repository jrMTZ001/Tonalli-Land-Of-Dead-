using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerCombatController : MonoBehaviour
{
    
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitBoxPos;
    private bool gotInput, isAttacking, isFirstAttack;
    private float lastInputTime = Mathf.NegativeInfinity;
    private Animator anim;
    
    
    [SerializeField]
    private LayerMask whatIsDamageable;
   

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableObject"))
        {
            // Llamamos al método OnHit para soltar el ítem
            collision.gameObject.GetComponent<InteractableObject>().OnHit();
        }
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }
    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gotInput = true;
            lastInputTime = Time.time;
        }
    }

    private void CheckAttacks()
    {
        if(gotInput)
        {
            if(!isAttacking)
            {
                gotInput= false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("Attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
                

            }
        }
        if(Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitbox()
    {
        /*
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);
        

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attack1Damage);
        }
      */
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            EnemyHealth enemy = collider.GetComponent<EnemyHealth>(); // Obtener el script de vida del enemigo
            if (enemy != null)
            {
                enemy.TakeDamage(Mathf.RoundToInt(attack1Damage)); // Convertir float a int antes de aplicar daño
            }

            // Si el objeto golpeado es una pared rompible
            BreakeableObject wall = collider.GetComponent<BreakeableObject>();
            if (wall != null)
            {
                wall.TakeDamage(1); // Llama al método para destruir la pared
            }
            // Llamar al método de OnHit del objeto interactuable
            InteractableObject interactable = enemy.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                interactable.OnHit();  // El objeto recibirá el golpe
            }
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("Attack1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
   
    
}
