﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AddPlayerCanvas : NetworkBehaviour
{
    public GameObject playerCanvas;

    void Start()
    {
        if (!isServer && isLocalPlayer)
        {
            GameObject go = Instantiate(playerCanvas, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            Image[] imgs = go.GetComponentsInChildren<Image>();

            foreach (Image img in imgs)
            {
                if (img.name == "PlayerImage")
                {
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    img.sprite = sr.sprite;
                    img.SetNativeSize();
                    RectTransform rt = img.rectTransform;

                    if (gameObject.name == "Player_Knight" || gameObject.name == "Player_Knight(Clone)")
                    {
                        rt.sizeDelta = new Vector2(rt.sizeDelta.x / 4, rt.sizeDelta.y / 4);
                    }
                    else if (gameObject.name == "Player_Dragon" || gameObject.name == "Player_Dragon(Clone)")
                    {
                        rt.sizeDelta = new Vector2(rt.sizeDelta.x / 4, rt.sizeDelta.y / 4);
                    }
                    else
                    {
                        rt.sizeDelta = new Vector2(rt.sizeDelta.x / 2, rt.sizeDelta.y / 2);
                    }
                    
                }
            }
        }
    }
}