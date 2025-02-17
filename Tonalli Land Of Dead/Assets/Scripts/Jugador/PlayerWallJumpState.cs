using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(CaballeroJaguar _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f;
        player.SetVelocity(5 * -player.facindDirection, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            player.stateMachine.ChangeState(player.airState);
        }
        if(player.IsGroundedDetected())
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
