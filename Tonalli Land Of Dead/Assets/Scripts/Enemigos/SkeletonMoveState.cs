using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(EnemyMove _enemyBase, EnemyStateMachine _stateMachine, string animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facindDirection, enemy.rb.velocity.y);
        if(enemy.IsWallDetected() || !enemy.IsGroundedDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
