using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(EnemyMove _enemyBase, EnemyStateMachine _stateMachine, string animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        /*
        if(enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(enemy.battleState);
        }
        */
    }
}
