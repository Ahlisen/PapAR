using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetPlayAudio : MonoBehaviour,
ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;
	private AudioSource audio;


	public Transform cube;

	void Start()
	{
		audio = GetComponent<AudioSource> ();
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	void Update() {
		float vol = MicInput.MicLoudness;
		if (vol > 0.01f) {
			cube.transform.localEulerAngles += Vector3.up * vol * 3f;
			Debug.Log(vol);
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
			StartCoroutine ("ScaleUp");
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
