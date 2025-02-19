using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMove : Entity
{
   // [SerializeField]protected LayerMask whatIsPlayer;
    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
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
    }

    //public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facindDirection, 50, whatIsPlayer);
}
