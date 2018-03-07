using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetDandelion : MonoBehaviour,
ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;
	private AudioSource audio;
	private Transform viewer;

	public Transform cube;
	//public Rigidbody dandelionTop;
	//public Terrain ter;
	public GameObject dandelionTop;

	WindReciever wR;

	Vector3 normScale;

	void Start()
	{
		audio = GetComponent<AudioSource> ();
		viewer = Camera.main.transform;
		Physics.gravity = new Vector3(0f,9.81f,0f);
		normScale = cube.transform.localScale;
		cube.transform.localScale *= 0.5f;
		wR = dandelionTop.transform.GetComponent<WindReciever> ();

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	void FixedUpdate() {
		/*float vol = MicInput.MicLoudness;
		if (vol > 0.01f) {
			dandelionTop.AddExplosionForce (vol * 100f, viewer.position, 10);
			//cube.transform.localEulerAngles += Vector3.up * vol * 3f;
			//Debug.Log(vol * 100f);
		}
		*/


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
			audio.Play();
			//wR.curAlpha = 0;
			//wR.curSize = wR.normSize;
			wR.curAlpha = 0.5f;
			wR.curSize *= 0.5f;
			//ter.enabled = true;
			StartCoroutine ("ScaleUp");
		}
		else
		{
			// Stop audio when target is lost
			audio.Stop();
			//ter.enabled = false;
			cube.transform.localScale *= 0.5f;
		}
	}


	IEnumerator ScaleUp () {
		float percent = 0.5f;
		//Vector3 normScale = cube.transform.localScale;

		while (percent < 1) {

			cube.transform.localScale = normScale * percent; //new Vector3(normScale.x, percent * normScale.y, normScale.z);
			//wR.curAlpha = percent;

			percent += 0.02f;
			yield return null;
		}

		cube.transform.localScale = normScale;
	}

}
