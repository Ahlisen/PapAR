using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TitleVoice : MonoBehaviour,
ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;
	private AudioSource audio;

	public GameObject playButton;
	//private Renderer rend;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		//rend = playButton.GetComponents<Renderer> ();
		//playButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		CheckTouch ();
	}

	private void CheckTouch() {

		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) {
			// Construct a ray from the current mouse coordinates
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				checkTouchHit(hit);
			}              
		}
		#endif
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
			//audio.Play();
			//playButton.SetActive(true);
			//StartCoroutine ("ScaleUp");
		}
		else
		{
			//playButton.SetActive(false);
			// Stop audio when target is lost
			//audio.Stop();
		}
	}

	private void checkTouchHit(RaycastHit hit) {
		if(hit.transform.gameObject.tag == "Play") {
			if (audio.isPlaying) {             
				audio.Stop();
			}
			else {
				audio.Play();
			}
		}
	}
}
