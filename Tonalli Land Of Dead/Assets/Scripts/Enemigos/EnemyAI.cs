using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
   
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    [Header("Player Detection")]
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    private bool isFacingRight = true;

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
        Patrol();
    }

    void Patrol()
    {
        if (!player)
        {
            rb.velocity = new Vector2(moveSpeed * (isFacingRight ? 1 : -1), rb.velocity.y);
            anim.SetBool("Move", true);

            if (IsWallDetected() || !IsGroundDetected())
            {
                Flip();
            }
        }
    }

    void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        if (playerCollider)
        {
            player = playerCollider.transform;
            ChasePlayer();
        }
        else
        {
            player = null;
        }
    }

    void ChasePlayer()
    {
        if (player)
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
            anim.SetBool("Move", true);

            if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
            {
                Flip();
            }

            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                rb.velocity = Vector2.zero;
                anim.SetTrigger("Attack");
            }
        }
    }

    bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
    }

    bool IsWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * (isFacingRight ? 1 : -1), 0.2f, groundLayer);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
