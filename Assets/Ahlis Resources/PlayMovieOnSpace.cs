using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovieOnSpace : MonoBehaviour {

	MovieTexture movie;
	AudioSource aud;
	public Texture firstFrame;
	bool virgin = true;
	Renderer r;


	void Awake() {
		r = GetComponent<Renderer>();
		movie = (MovieTexture)r.material.mainTexture;
		movie.loop = true;
		r.material.mainTexture = firstFrame;
		aud = GetComponent<AudioSource> ();
		aud.loop = true;
	}

	void Start() {
		
	}

	void Update () {
		CheckTouch ();
		/*
		if (Input.GetButtonDown ("Jump")) {

			if (movie.isPlaying) {
				movie.Stop();
				aud.Stop();
			}
			else {
				if (virgin) {
					r.material.mainTexture = movie;
					virgin = false;
				}
				movie.Play ();
				aud.Play ();
			}
		}
		*/
	}


	private void CheckTouch() {
		#if UNITY_ANDROID
		if(Input.touchCount > 0) {
		if (Input.GetTouch(0).phase == TouchPhase.Began) {
		// Construct a ray from the current touch coordinates
		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
		checkTouchHit(hit);
		}
		}
		}
		#endif

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

	private void checkTouchHit(RaycastHit hit) {
		if(hit.transform.gameObject.tag == "Finish") {
			if (movie.isPlaying) {             
				movie.Stop();
				aud.Stop();
			}
			else {
				if (virgin) {
					r.material.mainTexture = movie;
					virgin = false;
				}
				movie.Play();
				aud.Play ();
			}
		}
	}



}
