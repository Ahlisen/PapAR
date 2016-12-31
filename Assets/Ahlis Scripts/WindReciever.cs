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
		Physics.gravity = new Vector3(0f,9.81f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (MicInput.MicLoudness > 0.01f) {
			rig.AddExplosionForce (100f, viewer.position, MicInput.MicLoudness * 500f);
		}
		//rig.AddForceAtPosition( Vector3.left * 30, viewer.position);
	}
}
