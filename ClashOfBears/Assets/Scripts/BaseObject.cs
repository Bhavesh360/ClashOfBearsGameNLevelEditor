using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour {

    public float health;

    public void OnHit(float damageTaken)
    {
        health -= damageTaken;
        if(health<= 0)
        {
            Die();
        }
    }
    
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
