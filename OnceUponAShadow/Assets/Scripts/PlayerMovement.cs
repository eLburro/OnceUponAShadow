using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;


public class PlayerMovement : NetworkBehaviour
{
    [SyncVar]
    public bool m_FacingRight = false;

    private float m_MaxSpeed = 10f;
    private Rigidbody2D m_Rigidbody2D;
	private float jumping;
	private AudioSource audioSource;
	NetworkView nView;
    private Animator m_Anim;

    void Start () {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource> ();
		nView = GetComponent<NetworkView> ();
    }

	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

		if (Input.touchCount > 0 || Input.GetButtonDown("Fire1")) {
			Jump ();
			CmdPlaySound();
			playSound();
		}
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        Move(h);
    }

    private void Move(float move)
    {       
        /*if (move != 0)
        {
            m_Anim.SetBool("moving", true);
        }
        else
        {
            m_Anim.SetBool("moving", false);
        }*/

        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

        if (move > 0 && !m_FacingRight)
        {           
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
		
	void playSound()	{
		audioSource.volume = 1f;
		audioSource.Play();

	}

	// Play that soundsource over the network.
	[Command]
	void CmdPlaySound()	{
		audioSource.volume = 0.5f;
		audioSource.Play();

	}
}
