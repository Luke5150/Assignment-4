/*
 * (Luke Hensley)
 * (Prototype 3)
 * (Controls player movement)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float jumpForce;
    public ForceMode jumpForceMode;
    public float gravityModifier;

    public bool onGround = true;
    public bool gameOver = false;

    private Animator playerAnimator;

    public ParticleSystem explosion;
    public ParticleSystem dirt;

    public AudioClip jump;
    public AudioClip crash;

    public AudioSource playerEffects;


    
    void Start()
    {
        playerEffects = GetComponent<AudioSource>();

        playerAnimator = GetComponent<Animator>();

        playerAnimator.SetFloat("Speed_f", 1.0f);

        rb = GetComponent<Rigidbody>();

        jumpForceMode = ForceMode.Impulse;

        if (Physics.gravity.y > -10)
        {
            Physics.gravity *= gravityModifier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround && !gameOver)
        {
            playerEffects.PlayOneShot(jump, 1.0f);
            dirt.Stop();
            playerAnimator.SetTrigger("Jump_trig");
            rb.AddForce(Vector3.up * jumpForce, jumpForceMode);
            onGround = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            dirt.Play();
            onGround = true; 
        }

        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            playerEffects.PlayOneShot(crash, 1.0f);
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            explosion.Play();
            dirt.Stop();

            Debug.Log("Game Over!");
            gameOver = true;
        }
    }
}
