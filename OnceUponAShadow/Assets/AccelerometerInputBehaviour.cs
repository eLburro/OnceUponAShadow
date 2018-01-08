using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerInputBehaviour : MonoBehaviour {

	private Rigidbody2D rb;
	private AudioSource audioSource;
	public AudioClip scream;
	private float distance;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();
		Screen.autorotateToPortrait = true;
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//transform.Translate(0, 0, (Input.acceleration.z*-0.1f)+0.9f);
		distance = (((Input.acceleration.z-0.5f)*0.1f)+0.1f)+0.2f;
		transform.localScale = new Vector3(scale,scale, 0);
		transform.Translate(0, scale, 0);
		rb.velocity = new Vector2(6*Input.acceleration.x, -3*(Input.acceleration.z+0.5f));

		/*for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {*/
		if(Input.touchCount > 0) {
			audioSource.Play();
		//	}
		}
	}
}
