using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;


public class KnightActions : NetworkBehaviour
{
    private AudioSource audioSource;
    NetworkView nView;
    private Animator m_Anim;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nView = GetComponent<NetworkView>();
        m_Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        if (Input.touchCount > 0 || Input.GetButtonDown("Fire1"))
        {
            //Jump();
            CmdPlaySound();
            playSound();
        }
    }

    /*void Jump()
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 6);
    }*/

    /*public void playSound()
	{
		nView.RPC("rpcPlaySound", RPCMode.Others);
	}*/

    void playSound()
    {
        audioSource.volume = 1f;
        audioSource.Play();

    }

    // Play that soundsource over the network.
    [Command]
    void CmdPlaySound()
    {
        audioSource.volume = 0.5f;
        audioSource.Play();

    }
}
