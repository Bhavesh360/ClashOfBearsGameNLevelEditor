using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //make it serialzed so that you can see it in the interface even though the variable is private! :D
    [SerializeField]
    private float _Speed = 20f;

    private Unit _Owner;
    private Building _Target;
    public GameObject Explosion;

    public void Init(Building target, Unit Owner)
    {
        _Target = target;
        _Owner = Owner;
    }

    
	// Update is called once per frame
	void Update ()
    {
		if(_Target == null || _Target.health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if(Vector3.Distance(transform.position, _Target.transform.position) <= _Target.radius)
        {
            _Target.OnHit(_Owner.attackPower);
            //Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _Target.transform.position + Vector3.up, _Speed * Time.deltaTime);
	}
}
