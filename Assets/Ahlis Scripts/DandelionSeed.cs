using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandelionSeed : MonoBehaviour {

	/*
	Rigidbody rig2;
	float nextUpdate;
	*/

	WindZone wind;

	// Use this for initialization
	void Start () {

		wind = GetComponent<WindZone> ();
		/*
		rig2 = GetComponent<Rigidbody> ();
		rig2.isKinematic = true;
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

		//transform.eulerAngles = Camera.main.transform.position - transform.position;


		Vector3 relativePos = transform.position - Camera.main.transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;


		/*float m = MicInput.MicLoudness;
		//Debug.Log ("ss");
		if (m > 0.05f ) {
			Debug.Log ("ll");
			rig2.isKinematic = false;
			rig2.AddExplosionForce (m * 2f, Camera.main.transform.position, 100f);
		}

		if (Time.time > nextUpdate) {
			Vector3 f = new Vector3 (Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);
			f *= 20f;
			rig2.AddForceAtPosition (f, f);
			nextUpdate = Time.time + 1f;
		
		}
	
	 */
	}
}
