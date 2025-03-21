using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttack;
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(CaballeroJaguar _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        /*
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttack + comboWindow)
        {
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);
        #region Escoger direccion para atacar.
        float attackDir = player.facindDirection;
        if(xInput != 0)
        {
            attackDir = xInput;
        }
        #endregion
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        stateTimer = .1f;
        */
        base.Enter();

        if (comboCounter > 2 || Time.time >= lastTimeAttack + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);

        #region Escoger direcci�n para atacar
        float attackDir = player.facindDirection;
        if (xInput != 0)
        {
            attackDir = xInput;
        }
        #endregion

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        stateTimer = .1f;

        // **ATAQUE: Detectar enemigos y hacer da�o**
        Attack();
    }
    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .1f);
        comboCounter++;
        lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            player.ZeroVelocity();
        }
        if(triggerCalled)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
    private void Attack()
    {
        /*
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.attackPoint.position, player.attackRange, player.enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCalavera>()?.TakeDamage(20); // Inflige da�o
        }
        */
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.attackPoint.position, player.attackRange, player.enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Si el enemigo es una Calavera, le hace da�o
            EnemyCalavera calavera = enemy.GetComponent<EnemyCalavera>();
            if (calavera != null)
            {
                calavera.TakeDamage(20);
            }

            // Si el enemigo es un Alma Errante, lo mata de un golpe
            AlmaErrante almaErrante = enemy.GetComponent<AlmaErrante>();
            if (almaErrante != null)
            {
                almaErrante.TakeDamage(1); // Muere de un golpe
            }

            MonsterEnemy monsterEnemy = enemy.GetComponent<MonsterEnemy>();
            if(monsterEnemy != null)
            {
                monsterEnemy.TakeDamage(1);
            }
        }
    }
}
