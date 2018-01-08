using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

	public Text textF;

	// Use this for initialization
	void Start () {
		textF = GetComponent <Text> ();
	}

	// Update is called once per frame
	void Update () {
		float scale = (((Input.acceleration.z-0.5f)*0.1f)+0.1f)+0.2f;
		textF.text = "inputX = "+Input.acceleration.x;
		textF.text += "\ninputY = "+Input.acceleration.y;
		textF.text += "\ninputZ = "+Input.acceleration.z;
		textF.text += "\ntouchCount = "+Input.touchCount;
		textF.text += "\nscale = "+scale;
	}

}
