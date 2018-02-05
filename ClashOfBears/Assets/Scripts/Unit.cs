using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public abstract class Unit : BaseObject
{

    #region Globals

    public float speed = 5f;
    public float rotationSpeed = 300f;
    public float waypointDistanceToSwitch = 0.1f;
    public float attackPower = 20;
    public float attackRange = 1;
    
    [Tooltip("Time to wait between attacks")]
    public float attackWait = 1;

    internal Path currentPath;
    internal Building currentTarget;
    internal StateMachine<Unit> stateMachine = new StateMachine<Unit>();
    internal float sqrDistToSwitch;
    internal Seeker seeker;
    internal float sqrAttackRange;
    internal float nextAttack;

    internal Animator anim;
    internal Building prevTarget;

    private IEnumerator currentState;
    private Color prevcolor;
    

    #endregion

    #region Unity_BuiltIns

    // Use this for initialization
    void Start()
    {
        sqrAttackRange = attackRange * attackRange;
        anim = GetComponentInChildren<Animator>();
        seeker = GetComponent<Seeker>();
        sqrDistToSwitch = waypointDistanceToSwitch * waypointDistanceToSwitch;
        //seeker.StartPath(transform.position, currentTarget.position);
        stateMachine.SetState(new OnIdle(this));
    }

    private void Update()
    {
        nextAttack = nextAttack - Time.deltaTime < 0 ? 0 : nextAttack- Time.deltaTime;
        stateMachine.UpdateState();
    }
    //this is for animations
    Vector3 lastPos = Vector3.zero;
    private void FixedUpdate()
    {
        float speed = Vector3.SqrMagnitude(transform.position - lastPos) / Time.fixedDeltaTime;
        lastPos = transform.position;
        anim.SetFloat("Speed", speed);
    }

    #endregion

    public virtual void Attack()
    {
        //overriding abstract class Attack in unit
        Debug.Log("Attacking  ------");
        nextAttack = attackWait;
        anim.SetTrigger("Attack");
        prevTarget = currentTarget;
        //currentTarget.OnHit(attackPower);
    }

    public void IncreaseSpeed()
    {
        speed = speed + 20;

        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
    }

    public abstract void AttackAnimationEvent();

    //public override void Die()
    //{
    //    //do fancy looking things 

    //    //then destroy;
    //    //base.Die();
    //    Destroy(gameObject);
    //}
}

//#region StateMachine

//private void SetState(IEnumerator newState)
//{
//    if (currentState != null)
//        StopCoroutine(currentState);

//    currentState = newState;
//    StartCoroutine(currentState);
//}

//IEnumerator OnIdle()
//{
//    //Vector3.SqrMagnitude
//    currentPath = null;
//    while (currentTarget == null)
//    {
//        FindBuilding();
//        yield return null;
//    }

//    //print(currentTarget.name);
//    //if (Vector3.SqrMagnitude(transform.position - currentTarget.position) > sqrDistToSwitch)
//    //{
//    seeker.StartPath(transform.position, currentTarget.position, OnPathCompleted);
//    //}
//    //else
//    //{
//    //    SetState(OnIdle());
//    //}
//}

//IEnumerator OnMoving()
//{
//    int waypointIndex = 0;

//    while (currentTarget != null)
//    {
//        //Movement
//        Vector3 targetPos = currentPath.vectorPath[waypointIndex];
//        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);

//        //Rotation
//        Vector3 dir = targetPos - transform.position;
//        if (dir != Vector3.zero)
//        {
//            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.fixedDeltaTime);
//        }

//        //Distance Checking
//        if (Vector3.SqrMagnitude(transform.position - targetPos) <= sqrDistToSwitch)
//        {
//            ++waypointIndex;
//            if (waypointIndex >= currentPath.vectorPath.Count)
//                SetState(OnIdle());
//        }

//        yield return new WaitForFixedUpdate();
//    }


//}

//#endregion