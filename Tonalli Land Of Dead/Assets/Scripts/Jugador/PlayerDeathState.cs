using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    private Animator anim;
    public PlayerDeathState(CaballeroJaguar _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0); // Detener movimiento
        // Activar animaci�n de muerte
        anim.SetTrigger("Death");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // Aqu� podr�as agregar un tiempo antes de reiniciar la escena o mostrar una pantalla de Game Over
    }
}
