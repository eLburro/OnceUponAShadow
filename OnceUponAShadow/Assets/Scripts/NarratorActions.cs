using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NarratorActions : NetworkBehaviour
{
    public GameObject actionCanvas;
    public Button releaseFireButton;

    private Button[] actionButtons;

    void Start () {
        GameObject go = Instantiate(actionCanvas, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        actionButtons = go.GetComponentsInChildren<Button>();
        
        foreach (Button btn in actionButtons)
        {
            // Release Fire
            if (btn.name == releaseFireButton.name) btn.onClick.AddListener(() => ReleaseFire());
        }
    }
	
    void ReleaseFire()
    {
        Debug.Log("Release Fire");
    }
}