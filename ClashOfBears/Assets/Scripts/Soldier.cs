using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
   

    public override void AttackAnimationEvent()
    {
            currentTarget.OnHit(attackPower);
            print("UnitAttackAnimationEvent: " + transform.name);

    }


}

