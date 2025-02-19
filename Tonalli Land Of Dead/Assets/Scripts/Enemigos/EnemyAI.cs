using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    private bool isFacingRight = true;

    [Header("Detección del Jugador")]
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    private bool isAttacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        rb.velocity = new Vector2(moveSpeed * (isFacingRight ? 1 : -1), rb.velocity.y);

        bool isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
        bool isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * (isFacingRight ? 1 : -1), 0.2f, groundLayer);

        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
    }

    private void DetectPlayer()
    {
        Collider2D playerDetected = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (playerDetected)
        {
            player = playerDetected.transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        isAttacking = false;
        anim.SetBool("Attack", false);
        float direction = player.position.x > transform.position.x ? 1 : -1;

        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }

        rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
    }

    private void Attack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;
        anim.SetBool("Attack", true);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
