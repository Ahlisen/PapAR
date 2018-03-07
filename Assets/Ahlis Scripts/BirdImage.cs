using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BirdImage : MonoBehaviour,
ITrackableEventHandler {

	Transform viewer;
	AudioSource chirp;
	AudioSource flyAway;
	bool seen = false;
	bool flew = false;
	public Bird[] birds = new Bird[7];
	private TrackableBehaviour mTrackableBehaviour;

	void Start () {
		viewer = Camera.main.transform;

		AudioSource[] src = GetComponents<AudioSource> ();
		chirp = src [0];
		flyAway = src [1];

		chirp.loop = true;

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			// Play audio when target is found
			StartCoroutine(birdsEnter());


		}
		else
		{
			// Stop audio when target is lost
			seen = false;
			flew = false;
			chirp.Pause();
			foreach (Bird b in birds) {
				//b.transform.position = b.startPos + new Vector3 (5f, 5f, 5f);
				b.flying = false;
				b.speed = 1;
			}
		}
	}

	IEnumerator birdsEnter() {
		seen = true;
		flyAway.Play ();
		foreach (Bird b in birds) {
			StartCoroutine (b.flyIn (Random.Range (0.7f, 1.3f)));

		}

		yield return new WaitForSeconds (1.5f);
		chirp.Play ();

	
	}
	
	// Update is called once per frame
	void Update () {
		if (seen && !flew) {
			float dist = (viewer.position - transform.position).sqrMagnitude;
			//Debug.Log (dist);
			if (dist < 4f) {
				chirp.Stop ();
				flyAway.Play ();
				flew = true;
				foreach (Bird b in birds) {
					b.flying = true;
					b.speed = 5;
				}
			}
		
		}
	}
}
