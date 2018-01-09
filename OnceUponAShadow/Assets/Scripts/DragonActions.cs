using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class DragonActions : NetworkBehaviour
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
			SpitFire ();
		}
	}

	void PlaySound()
	{
		audioSource.volume = 1f;
		audioSource.Play();

	}

	void SpitFire() {
		PlaySound ();
		CmdPlaySound ();
	}

	// Play that soundsource over the network.
	[Command]
	void CmdPlaySound()
	{
		audioSource.volume = 0.5f;
		audioSource.Play();

	}
}
