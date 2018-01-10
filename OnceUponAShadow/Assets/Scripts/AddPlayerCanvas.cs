using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AddPlayerCanvas : NetworkBehaviour
{
    public GameObject playerCanvas;

    void Start()
    {
        if (!isServer)
        {
            GameObject go = Instantiate(playerCanvas, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
    }
}