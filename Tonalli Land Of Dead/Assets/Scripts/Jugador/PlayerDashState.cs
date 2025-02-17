using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(CaballeroJaguar _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y );
    }
    public override void Update()
    {
        base.Update();
        if (!player.IsGroundedDetected() && player.IsWallDetected())
        {
            player.stateMachine.ChangeState(player.wallSlide);
        }
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if(stateTimer < 0)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
