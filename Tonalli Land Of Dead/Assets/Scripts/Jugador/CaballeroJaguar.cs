using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroJaguar : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed = 10f;
    public float jumpForce = 8f;
    public float dashSpeed;
    public float dashDuration;
    [Header("CollsionInfo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facindDirection { get; private set; } = 1;
    private bool facingRight = true;
    #region Componentes
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region states
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; } 
    public PlayerDashState dashState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Saltar");
        airState = new PlayerAirState(this, stateMachine, "Saltar");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }

    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        
        anim = GetComponent<Animator>();
        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }
    private void CheckForDashInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(dashState);
        }
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    public bool IsGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
 
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance ));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        facindDirection = facindDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }
    public void FlipController(float x)
    {
        if(x > 0 && !facingRight)
        {
            Flip();
        }
        else if(x < 0 && facingRight)
        {
            Flip();
        }
    }
}
