using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnIdle : State<Unit>
{
    //runs before changind state.base(owner) is my parent abstract class constructor. 
    public OnIdle(Unit owner) : base(owner)
    {

    }
    public override void EnterState()
    {
        _Owner.currentPath = null;
        FindBuilding_Fast();
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if(_Owner.currentTarget == null)
        {
            FindBuilding_Fast();
        }
        else
        {
            _Owner.stateMachine.SetState(new OnMoving(_Owner));
        }
    }

    void FindBuilding_Linq() //gets the closes building in the list (using Linq)
    {
        _Owner.currentTarget = GameManager.instance.buildings.Where(t => t.health >0).OrderBy(t => Vector3.SqrMagnitude(t.transform.position - _Owner.transform.position)).FirstOrDefault();
    }

    void FindBuilding_Fast() //gets the closest building in the list (fast)
    {
        if (GameManager.instance.buildings.Count > 0)
        {
            float min = float.MaxValue;
            Building closest = null;

            foreach (var b in GameManager.instance.buildings)
            {
                float temp = Vector3.SqrMagnitude(_Owner.transform.position - b.transform.position);
                if(temp < min)
                {
                    min = temp;
                    closest = b;
                }
            }

            _Owner.currentTarget = closest;
            
            //print(currentTarget);
        }
    }
}
