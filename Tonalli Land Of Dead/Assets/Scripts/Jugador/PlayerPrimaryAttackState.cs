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
}
