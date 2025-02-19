using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected EnemyMove enemyBase;
   // protected Rigidbody2D ThRB;

    protected bool triggerCaled;
    private string animBoolName;
    protected float stateTimer;

    public EnemyState(EnemyMove _enemyBase, EnemyStateMachine _stateMachine, string animBoolName )
    {  

        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void Enter()
    {
        triggerCaled = false;
        //ThRB = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }
}
