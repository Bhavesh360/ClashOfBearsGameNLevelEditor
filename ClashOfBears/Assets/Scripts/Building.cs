
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BaseObject
{
    public float radius;


    // Use this for initialization
    protected void Start()
    {
        GameManager.instance.buildings.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.buildings.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

