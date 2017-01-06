using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageVideo : MonoBehaviour,
ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;
	public GameObject vid;
	private Renderer rend;

	void Start()
	{
		rend = vid.transform.GetComponent<Renderer> ();
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();

		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		Color newColor = new Color(1, 1f, 1, 0);
		rend.material.color = newColor;
	}
	
	// Update is called once per frame
	void Update () {
		
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
			StartCoroutine(FadeTo(1f,1f));
		}
		else
		{
			// Stop audio when target is lost
			//StartCoroutine(FadeTo(1f,1f));
			Color newColor = new Color(1, 1f, 1, 0);
			rend.material.color = newColor;
		}
	}


	IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = rend.material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Debug.Log (t);
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			rend.material.color = newColor;
			yield return null;
		}
	}

}
