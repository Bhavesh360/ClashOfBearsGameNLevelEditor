using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {

    public float distance = 10;
	
	// Update is called once per frame
	void Update () {

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = r.GetPoint(distance);
        transform.position = pos;
		
	}
}
