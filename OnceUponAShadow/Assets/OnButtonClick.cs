using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour {

	// Initialise Renderers and scene
	GameObject welcomePanel;
	GameObject hostPanel;
	GameObject controlPanel;

	void Start () {

		// find required GameObjects
		welcomePanel = GameObject.Find("panel-welcome");
		hostPanel = GameObject.Find("panel-host");
		controlPanel = GameObject.Find("panel-controls");

		Button openHostPanel = GameObject.Find("host-button").GetComponent<Button>();
		openHostPanel.onClick.AddListener(showHostPanel);

		Button openControlPanel = GameObject.Find("play-button").GetComponent<Button>();
		openControlPanel.onClick.AddListener(showControlPanel);

	}

	// shows the host panel and hides the others
	void showHostPanel() {
		welcomePanel.SetActive(false);
		controlPanel.SetActive(false);
		hostPanel.SetActive(true);
	}

	// shows the control panel and hides the others
	void showControlPanel() {
		welcomePanel.SetActive(false);
		hostPanel.SetActive(false);
		controlPanel.SetActive(true);
	}

	// Update is called once per frame
	void Update () {

	}
}