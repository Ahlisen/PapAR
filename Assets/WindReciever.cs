using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindReciever : MonoBehaviour {

	Rigidbody rig;
	Transform viewer;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
		viewer = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		rig.AddExplosionForce (100f, viewer.position, 10f);
		//rig.AddForceAtPosition( Vector3.left * 30, viewer.position);
	}
}
