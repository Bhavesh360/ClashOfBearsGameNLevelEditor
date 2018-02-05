using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit {

    [SerializeField]
    private Transform arrowSpawnPoint;

    [SerializeField]
    private Arrow arrowPrefab;


    public override void AttackAnimationEvent()
    {
            Arrow arrowClone = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation);
            arrowClone.Init(currentTarget, this);
            arrowClone.transform.LookAt(currentTarget.transform);
    }
}
