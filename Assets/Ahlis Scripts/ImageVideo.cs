using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageVideo : MonoBehaviour,
ITrackableEventHandler {
	/*
	MovieTexture movie;
	AudioSource aud;
	bool virgin = true;
	public Texture firstFrame;
	*/
	public GameObject vid;
	private Renderer rend;


	private TrackableBehaviour mTrackableBehaviour;

	void Start()
	{
		
		rend = vid.transform.GetComponent<Renderer> ();
		/*
		movie = (MovieTexture)rend.material.mainTexture;
		aud = vid.GetComponent<AudioSource> ();
		*/
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		Color newColor = new Color(1, 1f, 1, 0);
		rend.material.color = newColor;
		//rend.material.mainTexture = firstFrame;

	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			StartCoroutine(FadeTo(1f,1f));
		}
		else
		{	
				
			Color newColor = new Color(1, 1f, 1, 0);
			rend.material.color = newColor;
			/*
			rend.material.mainTexture = firstFrame;
			virgin = true;
			aud.Stop ();
			movie.Stop ();
			*/
		}
	}

		
	IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = rend.material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			//Debug.Log (t);
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			rend.material.color = newColor;
			yield return null;
		}
		/*
		if (virgin) {
			rend.material.mainTexture = movie;
			virgin = false;
		}
		aud.Play ();
		movie.Play ();
		*/

	}
 
}
