using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(CaballeroJaguar _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.stateMachine.ChangeState(player.primaryAttack);
        }

        if(!player.IsGroundedDetected())
        {
            player.stateMachine.ChangeState(player.airState);
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && player.IsGroundedDetected())
        {
            player.stateMachine.ChangeState(player.jumpState);
        }
    }
}
