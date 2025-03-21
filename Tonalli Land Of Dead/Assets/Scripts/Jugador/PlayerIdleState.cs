using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(CaballeroJaguar _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(xInput == player.facindDirection && player.IsWallDetected())
        {
            return;
        }
        if (xInput != 0 && !player.isBusy)
              player.stateMachine.ChangeState(player.moveState);
        
    }
}
