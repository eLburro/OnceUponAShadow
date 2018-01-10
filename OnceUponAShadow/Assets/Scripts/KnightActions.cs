using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;


public class KnightActions : NetworkBehaviour{
    private AudioSource audioSource;
	private AudioClip screamAudioClip;
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
	}

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
		if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
		{
            DrawSword();
            CmdPlaySound();
            playSound();
        }

		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;

		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold) {
			putOutFire ();
		}	
	}

	void putOutFire() {
		m_Anim.SetBool ("onFire", false);
	}

	public void catchFire() {
		m_Anim.SetBool ("onFire", true);
        Handheld.Vibrate();
    }

	void DrawSword() {
		if (m_Anim.GetBool ("swordOut") == true) {
			m_Anim.SetBool ("swordOut", false);
		} else {
			m_Anim.SetBool ("swordOut", true);
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
