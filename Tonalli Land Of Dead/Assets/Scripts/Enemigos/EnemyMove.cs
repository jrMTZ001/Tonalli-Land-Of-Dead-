using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : Entity
{
    [SerializeField]protected LayerMask whatIsPlayer;
    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector]public float lastTimeAttack;
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Start();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {   
        base.Update();
        stateMachine.currentState.Update();
        Debug.Log(IsPlayerDetected().collider.gameObject.name + "I SEE");
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facindDirection, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facindDirection, transform.position.y));
    }
}
