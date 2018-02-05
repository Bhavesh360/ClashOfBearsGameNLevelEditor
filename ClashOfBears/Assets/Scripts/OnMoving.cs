using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMoving : State<Unit>
{
    private int waypointIndex;
    private float sqrAttackDst;

    public OnMoving(Unit owner) : base(owner)
    {
    }

    public override void EnterState()
    {
        waypointIndex = 0;
        sqrAttackDst = Mathf.Pow(_Owner.currentTarget.radius, 2);
        _Owner.currentPath = null;
        _Owner.seeker.StartPath(_Owner.transform.position, _Owner.currentTarget.transform.position, OnPathCompleted);
    }

    public override void ExitState()
    {
       
    }

    public override void UpdateState()
    {
        if(_Owner.currentTarget == null)
        {
            _Owner.stateMachine.SetState(new OnIdle(_Owner));
            return;
        }

        if (_Owner.currentPath == null)
        {
            return;
        }

        //Movement
        Vector3 targetPos = _Owner.currentPath.vectorPath[waypointIndex];
        _Owner.transform.position = Vector3.MoveTowards(_Owner.transform.position, targetPos, _Owner.speed * Time.fixedDeltaTime);

        //Rotation
        Vector3 dir = targetPos - _Owner.transform.position;
        if (dir != Vector3.zero)
        {
            _Owner.transform.rotation = Quaternion.RotateTowards(_Owner.transform.rotation, Quaternion.LookRotation(dir), _Owner.rotationSpeed * Time.fixedDeltaTime);
        }

        //Check Distance to attack
        if(Vector3.SqrMagnitude(_Owner.transform.position - _Owner.currentTarget.transform.position) < sqrAttackDst + _Owner.sqrAttackRange)
        {
            _Owner.stateMachine.SetState(new OnAttacking(_Owner));
            return;
        }

        //Distance Checking
        if (Vector3.SqrMagnitude(_Owner.transform.position - targetPos) <= _Owner.sqrDistToSwitch)
        {
            ++waypointIndex;
            if (waypointIndex >= _Owner.currentPath.vectorPath.Count)
                _Owner.stateMachine.SetState(new OnIdle(_Owner));
        }

    }


    void OnPathCompleted(Path p)
    {
        _Owner.currentPath = p;
    
    }

}
