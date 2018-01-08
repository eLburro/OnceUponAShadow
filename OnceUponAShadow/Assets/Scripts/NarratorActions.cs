using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NarratorActions : NetworkBehaviour
{
    public GameObject actionCanvas;
    public Button releaseFireButton;
    public Button openGateButton;

    private Button[] actionButtons;

    void Start () {
        GameObject go = Instantiate(actionCanvas, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        actionButtons = go.GetComponentsInChildren<Button>();
        
        foreach (Button btn in actionButtons)
        {
            // Release Fire
            if (btn.name == releaseFireButton.name) btn.onClick.AddListener(() => ReleaseFire());

            // Open Castle Gate
            if (btn.name == openGateButton.name) btn.onClick.AddListener(() => OpenCastleGate());
        }
    }
	
    void ReleaseFire()
    {
        Debug.Log("Release Fire");
    }

    void OpenCastleGate()
    {
        Debug.Log("Open Gate");

        GameObject go = GameObject.Find("Sprite_Castle");
        Animator m_GateAnim = go.GetComponent<Animator>();

        m_GateAnim.SetBool("isOpen", true);
    }
}