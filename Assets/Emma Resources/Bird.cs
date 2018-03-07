using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
	
	private float movementSpeed;
	public Vector3 startPos;
	public float speed = 1;
	public bool flying = false;
	private Renderer rend;
	int dir;

	void Start () {
		movementSpeed = Random.Range (1f, 3f);
		startPos = transform.position;
		//transform.position = startPos + new Vector3 (Random.Range (-3f, -2f)*dir, 1f, Random.Range (0.5f, 1.5f));
		rend = GetComponent<Renderer> ();
		//Color newColor = Color.HSVToRGB (Random.value, 1, 1);
		//rend.material.color = newColor;

		int[] choices = {-1,1};
		dir = choices [Random.Range (0, 2)];
		rend.material.mainTextureScale = new Vector2 (dir, 1);
		//transform.localScale *= Random.Range(0.7f, 1.3f);
	}


	void Update () {
		transform.rotation = Quaternion.Euler(90,0, Mathf.Sin(movementSpeed + Time.realtimeSinceStartup * speed) * 15);

		/*
		if (Input.GetKeyDown ("space")) {
			//speed = 5;
			//flying = true;
			StartCoroutine(flyIn(Random.Range(0.5f,2f)));
			//Color newColor = Color.HSVToRGB (Random.value, 1, 1);
			//rend.material.color = newColor;
		}
		*/

		if (flying) {
			transform.Translate(new Vector3(1*dir,0,0) * movementSpeed * Time.deltaTime);
		}
			
	}

	public IEnumerator flyIn (float seconds) {

		float percent = 0;
		Vector3 flyPos = startPos + new Vector3 (Random.Range (-3f, -2f)*dir, 1f, Random.Range (0.5f, 1.5f));
		//transform.position = flyPos;
		//yield return new WaitForSeconds (Random.Range (0f, 1f));

		while (percent < 1) {
			
			percent += Time.deltaTime / seconds; // (0.05f / transform.localScale.x);
			transform.position = Vector3.Lerp (flyPos, startPos, Mathf.SmoothStep(0, 1, percent));

			yield return null;
		}


	}
}
