using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NarratorActions : NetworkBehaviour
{
    public GameObject actionCanvas;
    public Button openGateButton;
    public Button killDragonButton;

    private Button[] actionButtons;
    private Toggle toggle;

    void Start () {
        GameObject go = Instantiate(actionCanvas, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        actionButtons = go.GetComponentsInChildren<Button>();
        toggle = go.GetComponentInChildren<Toggle>();

        foreach (Button btn in actionButtons)
        {
            // Open Castle Gate
            if (btn.name == openGateButton.name) btn.onClick.AddListener(() => OpenCastleGate());

            // Kill the dragon
            if (btn.name == killDragonButton.name) btn.onClick.AddListener(() => KillTheDragon());
        }

        toggle.onValueChanged.AddListener(delegate { MusicToggleValueChanged(toggle); });
    }

    void MusicToggleValueChanged(Toggle change)
    {
        if (toggle.isOn)
        {
            Debug.Log("Start Music");
        }
        else
        {
            Debug.Log("Stop Music");
        }
    }

    void KillTheDragon()
    {
        Debug.Log("Kill the Dragon");

        GameObject goBtn = GameObject.Find("KillTheDragon");
        Destroy(goBtn);

        GameObject go = GameObject.Find("Player_Dragon(Clone)");
        NetworkServer.Destroy(go);
    }

    void OpenCastleGate()
    {
        Debug.Log("Open Gate");

        GameObject goBtn = GameObject.Find("OpenCastleGate");
        Destroy(goBtn);

        // open gate
        GameObject go = GameObject.Find("Sprite_Castle");
        Animator m_GateAnim = go.GetComponent<Animator>();

        m_GateAnim.SetBool("isOpen", true);

        // release princess
        GameObject go2 = GameObject.Find("Env_Castle_Ground");
        NetworkServer.Destroy(go2);
    }
}