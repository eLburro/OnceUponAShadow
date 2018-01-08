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

    void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource> ();
    }

	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
			
		Move();

		if (Input.touchCount > 0 || Input.GetButtonDown("Fire1")) {
			Jump ();
			audioSource.Play();
		}
    }

    private void Move()
    {

		float xVel = CrossPlatformInputManager.GetAxis("Horizontal");
		if(Input.accelerationEventCount > 0 && Mathf.Abs(Input.acceleration.x) > 0) {
			xVel = 6 * Input.acceleration.x;
			m_Rigidbody2D.velocity = new Vector2(xVel * m_MaxSpeed, m_Rigidbody2D.velocity.y);
		}
		else m_Rigidbody2D.velocity = new Vector2(xVel * m_MaxSpeed, m_Rigidbody2D.velocity.y);

        if (xVel > 0 && !m_FacingRight)
        {           
            Flip();
        }
        else if (xVel < 0 && m_FacingRight)
        {
            Flip();
        }
    }

	void Jump() {
		m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,6);
	}

    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
