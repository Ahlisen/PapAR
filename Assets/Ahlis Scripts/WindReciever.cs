using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindReciever : MonoBehaviour {

	Rigidbody rig;
	Transform viewer;
	ParticleSystem ps;
	WindZone wind;

	public GameObject sphere;
	Renderer spRend;
	public Vector3 normSize;
	public Vector3 curSize;
	public float curAlpha = 1;

	// Use this for initialization
	void Start () {
		normSize = sphere.transform.localScale;
		spRend = sphere.transform.GetComponent<Renderer> ();
		curSize = normSize;
		rig = GetComponent<Rigidbody> ();
		viewer = Camera.main.transform;
		Physics.gravity = new Vector3(0f,9.81f,0f);
		ps = GetComponent<ParticleSystem> ();
		wind = GetComponent<WindZone> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

		float vol = MicInput.MicLoudness;
		if (vol > 0.01f) {
			
			curSize *= 0.97f;
			curAlpha *= 0.97f;

			Vector3 relativePos = transform.position - viewer.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = rotation;
			rig.AddExplosionForce (vol * 25f, viewer.position, 10f);
			if (curSize.sqrMagnitude > normSize.sqrMagnitude * 0.1f) {
				ps.Emit ((int)(vol * 20f));
			}

			wind.windMain = vol * 20f;
			//Debug.Log (vol);
		} else {
			wind.windMain = 0;
			if (curSize.sqrMagnitude < normSize.sqrMagnitude) {
				curSize *= 1.015f;
				curAlpha *= 1.015f;

			}
		}
		Color newColor = new Color(1, 1, 1, curAlpha);
		spRend.material.color = newColor;
		sphere.transform.localScale = curSize;



		//rig.AddForceAtPosition( Vector3.left * 30, viewer.position);
	}
}
