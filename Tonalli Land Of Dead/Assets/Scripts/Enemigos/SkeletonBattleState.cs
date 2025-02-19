using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private EnemySkeleton enemy;
    private int moveDirection;

    public SkeletonBattleState(EnemyMove _enemyBase, EnemyStateMachine _stateMachine, string animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected())
        {
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                Debug.Log("Attack");
                enemy.ZeroVelocity();
                return;
            }
        }

        if (player.position.x > enemy.transform.position.x)
        {
            moveDirection = 1;
        }
        else if(player.position.x < enemy.transform.position.x)
        {
            moveDirection = -1;
        }
        enemy.SetVelocity(enemy.moveSpeed * moveDirection, ThRB.velocity.y);
        
    }
}
