using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PrincessActions : NetworkBehaviour	{
	private AudioSource audioSource;
	private Animator m_Anim;

	//shake gesture
	float accelerometerUpdateInterval = 1.0f / 60.0f;
	float lowPassKernelWidthInSeconds = 2.0f;
	float shakeDetectionThreshold = 4.0f;
	float lowPassFilterFactor;
	Vector3 lowPassValue;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		m_Anim = GetComponent<Animator>();

		//shake gesture
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;	

		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;

		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
		{
			//putOutFire ();
		}	
	}

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
		{
			m_Anim.SetBool ("isScreaming", true);
			CmdPlaySound();
			playSound();
			//yield return new WaitForSeconds(2);
			m_Anim.SetBool ("isScreaming", false);
		}
	}
		
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
