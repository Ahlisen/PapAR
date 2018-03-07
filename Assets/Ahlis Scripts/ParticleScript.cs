using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {

	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (MicInput.MicLoudness > 0.02f) {
			ps.Emit ((int)(10f * Time.fixedDeltaTime));
		}

	}
}
