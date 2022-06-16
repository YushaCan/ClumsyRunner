using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    private Animator playerAnim;
    public AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float speed;
    public float jumpForce;
    public bool doubleJumpEnabled = false;
    public TimeBar timeBar;
    public float powerupTime = 5f;
    public GameObject timeBarEnabled;
    [SerializeField] float downForce;
    [SerializeField] float gravityModifier;
    [SerializeField] float horizontalInput;
    [SerializeField] float turnSpeed;
    public bool gameOver = false;
    private float xRangeLeft = 6.7f;
    private float xRangeRight = -0.5f;
    public bool isOnGround = true;
    public bool isOnAir = false;
    [SerializeField] float speedUpTimer = 7.5f;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        timeBar.SetMaxTime(powerupTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameOver == false)
        {
            Cursor.visible = false;
            speedUpTimer -= Time.deltaTime;
            transform.Translate(UnityEngine.Vector3.forward * Time.deltaTime * speed); //Speed of the Player
            if (speedUpTimer <= 0)
            {
                speed += 0.25f;
                speedUpTimer = 1;
            }
            //Make player turn left and right
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(UnityEngine.Vector3.right * horizontalInput * Time.deltaTime * turnSpeed);
            //To avoid player to fall off the scene 
            if (transform.position.x > xRangeLeft)
            {
                transform.position = new UnityEngine.Vector3(transform.position.x - 0.14f, transform.position.y, transform.position.z);
            }
            if (transform.position.x < xRangeRight)
            {
                transform.position = new UnityEngine.Vector3(transform.position.x + 0.14f, transform.position.y, transform.position.z);
            }
        }
    }
    private void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver == false)
        {
            
            playerRb.AddForce(UnityEngine.Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            isOnAir = true;
            playerAudio.PlayOneShot(jumpSound);
        }
        //Double Jump
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && isOnAir && !gameOver && doubleJumpEnabled)
        {
            playerRb.AddForce((UnityEngine.Vector3.up * jumpForce) / 2, ForceMode.Impulse);
            isOnAir = false;
            playerAudio.PlayOneShot(jumpSound);
        }
        //Down Force Instantly
        if(Input.GetKeyDown(KeyCode.S) && !isOnGround && !gameOver)
        {
            playerRb.AddForce(UnityEngine.Vector3.down * downForce, ForceMode.Impulse);
        }
        //Enables Time Bar
        if(doubleJumpEnabled == true && !gameOver)
        {
            timeBarEnabled.SetActive(true);
            powerupTime -= Time.deltaTime;
            timeBar.SetTime(powerupTime);
        }
        else if (doubleJumpEnabled == false && !gameOver)
        {
            timeBarEnabled.SetActive(false);
            powerupTime = 5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
        isOnAir = false;
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Cursor.visible = true;
            playerAnim.SetBool("Death" , true);
            playerAudio.PlayOneShot(crashSound);
            explosionParticle.Play();
        }
    }
}
