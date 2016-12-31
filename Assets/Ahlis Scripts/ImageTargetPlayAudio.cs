using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetPlayAudio : MonoBehaviour,
ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;
	private AudioSource audio;
	private Transform viewer;

	public Transform cube;
	public Rigidbody dandelionTop;

	void Start()
	{
		audio = GetComponent<AudioSource> ();
		viewer = Camera.main.transform;
		Physics.gravity = new Vector3(0f,9.81f,0f);

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	void FixedUpdate() {
		float vol = MicInput.MicLoudness;
		if (vol > 0.01f) {
			dandelionTop.AddExplosionForce (vol * 100f, viewer.position, 10);
			//cube.transform.localEulerAngles += Vector3.up * vol * 3f;
			Debug.Log(vol * 100f);
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
			audio.Play();
			//StartCoroutine ("ScaleUp");
		}
		else
		{
			// Stop audio when target is lost
			audio.Stop();
		}
	}  

	IEnumerator ScaleUp () {
		float percent = 0;
		Vector3 normScale = cube.transform.localScale;

		while (percent < 1) {

			cube.transform.localScale = new Vector3(normScale.x, percent * normScale.y, normScale.z);

			percent += 0.01f;
			yield return null;
		}

		cube.transform.localScale = normScale;
	}

}
