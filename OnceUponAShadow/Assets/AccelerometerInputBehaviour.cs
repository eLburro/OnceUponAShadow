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
		distance = 0.45f-(Mathf.Max(-Input.acceleration.z,0.5f)/3);
		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(distance,distance, 0), Time.deltaTime);
		//transform.Translate(0, distance, 0);
		rb.velocity = new Vector2(6*Input.acceleration.x, distance); //-3*(Input.acceleration.z+0.5f)

		/*for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {*/
		if(Input.touchCount > 0) {
			audioSource.Play();
		//	}
		}
	}
}
