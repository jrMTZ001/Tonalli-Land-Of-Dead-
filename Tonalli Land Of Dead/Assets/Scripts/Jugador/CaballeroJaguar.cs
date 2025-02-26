using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroJaguar : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public bool isBusy {  get; private set; }
    [Header("Move Info")]
    public float moveSpeed = 10f;
    public float jumpForce = 8f;
    public Transform player;
    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    [Header("Salud")]
    //public int maxHealth = 100;
    //public int currentHealth;
    //public bool isDead = false;
    public int puntos = 0;
    //public int vidaMaxima = 100;
    //public int vidaActual;
    //public HealthBar healthBar;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private float knockbackForceX = 5f;
    [SerializeField] private float knockbackForceY = 3f;
    private bool isBeingKnockedBack = false;
    public float dashDir { get; private set; }
   

    #region states
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerDeathState deathState { get; private set; }
    #endregion

    protected override void Awake()
    {   
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Saltar");
        airState = new PlayerAirState(this, stateMachine, "Saltar");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "Slide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Saltar");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        deathState = new PlayerDeathState(this, stateMachine, "Death");
        
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        //currentHealth = maxHealth;
        //healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
    public void TakeDamage(int damage, Vector3 attackerPosition)
    {
        
        //currentHealth -= damage;
       // healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log("Jugador recibió daño: " + damage);

        // **Aplica Knockback con menor fuerza**
        Vector2 knockbackDir = (transform.position - attackerPosition).normalized;
        float knockbackForce = -3f; // Ajusta este valor (prueba con 1.5 o 2)
        rb.velocity = new Vector2(knockbackDir.x * knockbackForce, rb.velocity.y);
        anim.SetTrigger("Hurt");
        StartCoroutine(KnockbackRoutine(knockbackDir));
        /*
        if (currentHealth <= 0)
        {
            Die();
        }
        */
    }
    public void AgregarPuntos(int cantidad)
    {
        puntos += cantidad;
        Debug.Log("Puntos: " + puntos);
    }

    public void RecuperarSalud(int cantidad)
    {
        //currentHealth += cantidad;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log("Vida: " + vidaActual);
    }
    private IEnumerator KnockbackRoutine(Vector2 knockbackDirection)
    {
        isBeingKnockedBack = true;
        rb.velocity = Vector2.zero; // Resetear velocidad antes de aplicar knockback

        rb.AddForce(new Vector2(knockbackDirection.x * knockbackForceX, knockbackForceY), ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration); // Esperar un poco para el knockback

        isBeingKnockedBack = false; // Restaurar control del jugador
    }


    private void Die()
    {
        /*
        if (isDead) return;

        isDead = true;
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;

        rb.gravityScale = 2f; // Asegura que la gravedad sigue funcionando
        rb.constraints = RigidbodyConstraints2D.FreezePositionX; // Solo permite que caiga, pero no se mueva horizontalmente

        Debug.Log("Jugador ha muerto");
        */
        
        if (isDead) return;
        isDead = true;
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
        rb.simulated = false; // Desactiva la física del jugador si lo deseas
        

        StartCoroutine(RespawnRoutine()); // Inicia la reaparición después de la muerte
    
    }
    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(1.5f); // Espera 1.5 segundos antes de revivir
        LifeController.instance.Respawn();
    }
    protected override void Update()
    {  
        base.Update();
        Debug.Log("Jugador Muerto");
        //currentHealth = maxHealth;
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }
    private void CheckForDashInput()
    {
        if (IsWallDetected())
        {
            return;
        }
        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = facindDirection;
            }
            stateMachine.ChangeState(dashState);
        }
    }
        
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}

    
