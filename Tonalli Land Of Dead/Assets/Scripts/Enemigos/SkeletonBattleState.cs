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
        //player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        base.Update();

        if (enemy == null)
        {
            Debug.LogError("El enemigo en SkeletonBattleState es null.");
            return;
        }

        if (player == null)
        {
            Debug.LogError("El player no fue encontrado, no se puede calcular la dirección.");
            return;
        }

        moveDirection = (player.position.x > enemy.transform.position.x) ? 1 : -1;
        enemy.SetVelocity(enemy.moveSpeed * moveDirection, enemy.rb.velocity.y);
        /*
        if(player.position.x > enemy.transform.position.x)
        {
            moveDirection = 1;
        }
        else if(player.position.x < enemy.transform.position.x)
        {
            moveDirection = -1;
        }
        enemy.SetVelocity(enemy.moveSpeed * moveDirection, enemy.rb.velocity.y);
        */
    }
}
