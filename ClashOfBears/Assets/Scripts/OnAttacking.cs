using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttacking : State<Unit>
{
    public OnAttacking(Unit owner) : base(owner)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entering AttackState");
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if(_Owner.currentTarget == null || _Owner.currentTarget.health <= 0)
        {
            _Owner.anim.SetTrigger("StopAttack");
            _Owner.stateMachine.SetState(new OnIdle(_Owner));
            return;
        }
        if(_Owner.nextAttack <= 0)
        {
            _Owner.Attack();
        }
    }

    
}
