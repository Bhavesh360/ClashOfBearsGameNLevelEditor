using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhaveshUnit : Unit
{
    [SerializeField]
    private Transform BombSpawnPoint;

    [SerializeField]
    private Missile BombPrefab;

    public override void AttackAnimationEvent()
    {
        Missile MissileClone = Instantiate(BombPrefab, BombSpawnPoint.position, transform.rotation);
        MissileClone.Init(currentTarget, this);
        MissileClone.transform.LookAt(currentTarget.transform);
    }


}

