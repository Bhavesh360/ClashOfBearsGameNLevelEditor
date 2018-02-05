using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bhaveshbuilding : House
{
    public GameObject Particle;

    // Use this for initialization
    void Start () {
        base.Start();
        InvokeRepeating("KillBears", 2.0f, 2.0f);
    }
	
	// Update is called once per frame
	void Update () {

	}

    void KillBears()
    {
        AreaDamageEnemies(transform.position, 20, 50);
    }

    void AreaDamageEnemies(Vector3 location, float radius, float damage)
    {
        //getting all objects with a collider within specified radius
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        //For each object get all the units. If unit it not null then apply damage to it. 
        foreach(Collider col in objectsInRange)
        {
            Unit unit = col.GetComponent<Unit>();
            if(unit!=null)
            {
                float proximity = (location - unit.transform.position).magnitude;
                float effect = 1 - (proximity / radius);
                unit.OnHit(damage * effect);
                Instantiate(Particle, transform.position, transform.rotation);
            }
        }
    }
}
