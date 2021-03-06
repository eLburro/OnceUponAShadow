﻿using System.Collections;
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
	private Animator m_Anim;



    void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Anim = GetComponent<Animator>();
    }

	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
			
		Move();
    }

    private void Move()
    {
		float xVel = CrossPlatformInputManager.GetAxis("Horizontal");
		if(Input.accelerationEventCount > 0 && Mathf.Abs(Input.acceleration.x) > 0.12) {
			xVel = 3 * Input.acceleration.x;
			m_Rigidbody2D.velocity = new Vector2(xVel * m_MaxSpeed, m_Rigidbody2D.velocity.y);
		}
		else m_Rigidbody2D.velocity = new Vector2(xVel * m_MaxSpeed, m_Rigidbody2D.velocity.y);

        if (xVel > 0.4 && !m_FacingRight)
        {           
            Flip();
			CmdFlip ();
        }
        else if (xVel < -0.4 && m_FacingRight)
        {
            Flip();
			CmdFlip ();
        }

		if (xVel > 0.4 || xVel < -0.4) {
			m_Anim.SetBool("isMoving", true);
		} else {
			m_Anim.SetBool("isMoving", false);
		}
    }

    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	[Command]
	void CmdFlip()	{
		Flip ();
	}

}
