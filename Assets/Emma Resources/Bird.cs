using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	private int speed = 1;
	public float movementSpeed = 100;

	private bool flying = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt (Camera.main.transform.position * -1);
		transform.rotation = Quaternion.Euler(0,0, Mathf.Sin(Time.realtimeSinceStartup * speed) * 10);

		// Change to run Flying if camera is close enough
		if (Input.GetKeyDown ("space")) {
			flying = true;
		}

		if (flying) {
			Flying ();
		}
			
	}

	void Flying () {
		speed = 5;
		transform.Translate(new Vector3(1,0,0) * movementSpeed * Time.deltaTime);

	}
}
