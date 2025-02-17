using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine playerStateMachine;
    protected CaballeroJaguar player;
    protected Rigidbody2D rb;

    private string animBoolName;
    protected float xInput;

    protected float stateTimer;

    public PlayerState( CaballeroJaguar _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.playerStateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }
    public virtual void Update()
    {   
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
}
