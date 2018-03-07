using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TitleVoice : MonoBehaviour,
ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;
	private AudioSource audio;
	private AudioSource audioClick;

	public Texture playText;
	public Texture pauseText;

	public GameObject playButton;
	//public GameObject pauseButton;
	private Renderer playRend;


	//public GameObject twitterIcon;
	private Renderer twitterIconRend;
	private TextMesh twitterText;
	int twitterNum;
	int len = 4;

	public GameObject[] iconArray = new GameObject[4];
	Renderer[] iconRendArray = new Renderer[4];
	public GameObject[] iconTextArray = new GameObject[4];
	int[] numArray = new int[4];
	TextMesh[] textArray = new TextMesh[4];

	float timeBetweenUpdates = 3;
	float untilNext;


	bool changing = false;
	bool seen = false;
	//private Renderer rend;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < 4; ++i) {
			textArray [i] = iconTextArray [i].GetComponent<TextMesh> ();
			numArray[i] = Random.Range (0, 135);
			textArray[i].text = numArray[i].ToString();

			iconRendArray [i] = iconArray [i].GetComponent<Renderer> ();
		}

		playRend = playButton.GetComponentInChildren<Renderer>();
		playRend.material.mainTexture = playText;
		AudioSource[] aSources = GetComponents<AudioSource>(); 
		audio = aSources [0]; 
		audioClick = aSources [1];

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (seen && !changing) {
			CheckTouch ();
		}

		if (Time.time > untilNext) {
			for (int i = 0; i < 4; ++i) {
				numArray[i] += Random.Range (0, 2);
				textArray[i].text = numArray[i].ToString();
				untilNext = Time.time + timeBetweenUpdates;
			}
		}
	}

	private void CheckTouch() {

		#if UNITY_ANDROID
		if(Input.touchCount > 0) {
			if(Input.GetTouch(0).phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					checkTouchHit(hit);
					changing = true;
				} 
			}
		}
		#endif

		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				checkTouchHit(hit);
				changing = true;
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
			playButton.transform.localScale = new Vector3(playButton.transform.localScale.x, 0.01f, playButton.transform.localScale.z);
			Color newColor = new Color(1, 1, 1, 0);
			playRend.material.color = newColor;
			StartCoroutine(ButtonEnter (1f, 0.3f));
			for (int i = 0; i < 4; i++) {
				StartCoroutine (IconEnter (iconRendArray[i] , 1, 0.5f));
			}
			seen = true;
		}
		else
		{
			audio.Stop ();
			playRend.material.mainTexture = playText;
			seen = false;
		}
	}

	private void checkTouchHit(RaycastHit hit) {
		if(hit.transform.gameObject.tag == "Play") {
			if (audio.isPlaying) {
				StartCoroutine(ButtonPressed (0f, 0.1f, true));
				//audio.Stop();
			}
			else {
				StartCoroutine(ButtonPressed (0f, 0.1f, true));
				//audio.Play();
			}
		}
		/*
		if (hit.transform.gameObject.tag == "Pause") {
			if (audio.isPlaying) {
				StartCoroutine(ButtonPressed (0f, 0.1f, true));
				//StartCoroutine(ButtonPressedPause (0f, 0.1f, true));

				//audio.Stop();
			}
			else {
				StartCoroutine(ButtonPressed (0f, 0.1f, true));
				//audio.Play();
			}
		}
		*/
	}

	IEnumerator ButtonPressed(float aValue, float aTime, bool triggerAudio)
	{
		float ySize = playButton.transform.localScale.y;
		
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			//Debug.Log (t);
			Vector3 newSize = new Vector3 (playButton.transform.localScale.x, Mathf.Lerp (ySize, aValue, t), playButton.transform.localScale.z);
			playButton.transform.localScale = newSize;
			yield return null;
		}
		if (triggerAudio) {
			audioClick.Play ();
			if (audio.isPlaying) {
				playRend.material.mainTexture = playText;
				StartCoroutine (ButtonPressed (1f, 0.1f, false));
			} else {
				playRend.material.mainTexture = pauseText;
				StartCoroutine (ButtonPressed (0.5f, 0.05f, false));
			}
		} else {
			yield return new WaitForSeconds (0.1f);
			if (audio.isPlaying) {
				audio.Pause ();
			} else {
				audio.Play ();
			}
			changing = false;
		}
	}

	IEnumerator ButtonEnter(float aValue, float aTime)
	{
		float ySize = playButton.transform.localScale.y;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			//Debug.Log (t);
			Vector3 newSize = new Vector3 (playButton.transform.localScale.x, Mathf.Lerp (ySize, aValue, t), playButton.transform.localScale.z);
			playButton.transform.localScale = newSize;
			float b = Mathf.Lerp (0.5f, 1, t);
			Color newColor = new Color(b,b,b);//(1, 1, 1, Mathf.Lerp(0,1,t));
			//Color newColor = new Color(1, 1, 1, Mathf.Lerp(0,1,t));
			playRend.material.color = newColor;
			yield return null;
		}
	}

	IEnumerator IconEnter(Renderer r ,float aValue, float aTime)
	{

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(0 , aValue ,t));
			r.material.color = newColor;
			yield return null;
		}
	}

}
