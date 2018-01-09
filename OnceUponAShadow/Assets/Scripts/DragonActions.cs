using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class DragonActions : NetworkBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawn;

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
            SpitFire();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFireball();
        }
    }

    void PlaySound()
    {
        audioSource.volume = 1f;
        audioSource.Play();

    }

    void SpitFire()
    {
        // TODO merge spit fire & fireball
        PlaySound();
        CmdPlaySound();
        CmdFireball();
    }

    // Play that soundsource over the network.
    [Command]
    void CmdPlaySound()
    {
        audioSource.volume = 0.5f;
        audioSource.Play();

    }

    [Command]
    void CmdFireball()
    {
        var fireball = (GameObject)Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation);

        // get shooting direction
        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();

        // Add velocity to the fireball
        float veloX = (playerMovement.m_FacingRight) ? 20f : -20f;
        Rigidbody2D rigidbody2D = fireball.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(veloX, rigidbody2D.velocity.y);

        if (playerMovement.m_FacingRight)
        {
            Vector3 theScale = rigidbody2D.gameObject.transform.localScale;
            theScale.x *= -1;
            rigidbody2D.gameObject.transform.localScale = theScale;
        }

        NetworkServer.Spawn(fireball);

        // Destroy the fireball after half a second
        Destroy(fireball, 0.5f);
    }
}