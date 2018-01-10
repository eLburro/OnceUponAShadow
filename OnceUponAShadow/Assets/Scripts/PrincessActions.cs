using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PrincessActions : NetworkBehaviour	{
	private AudioSource audioSource;
	private Animator m_Anim;


	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		m_Anim = GetComponent<Animator>();

		m_Anim.SetBool ("isScreaming", false);
	}

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}
			
		if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine (Scream());
		}
	}
	IEnumerator Scream() {
		m_Anim.SetBool ("isScreaming", true);
		CmdPlaySound ();
		playSound ();
		yield return new WaitForSeconds(2);
		m_Anim.SetBool ("isScreaming", false);
	}

	void playSound()
	{
		audioSource.volume = 0.75f;
		audioSource.Play();
	}

	// Play that soundsource over the network.
	[Command]
	void CmdPlaySound()
	{
		audioSource.volume = 0.35f;
		audioSource.Play();

	}
}
