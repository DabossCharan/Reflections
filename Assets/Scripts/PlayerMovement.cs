using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    private float speed = 10f;
    private float jumpSpeed = 8f;
    private float horizontalInput;
    private bool isOnGround = true;
    private bool isMoving = false;
    private Rigidbody2D playerRb;
    private float xMin = -11f;
    private bool faceRight = true;
    private bool jump = false;
    private float jumpDelay;
    private SpriteRenderer playerSprite;

    // Reflect 
    private GameObject reflection;
    private SpriteRenderer reflectionSprite;
    private Animator reflectionAnim;

    // Animator
    private Animator playerAnim;

    // SFX
    private AudioSource playerAudio;
    public AudioClip jumpSFX;
    public AudioClip runSFX;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        reflection = GameObject.FindGameObjectWithTag("Reflection");
        playerSprite = GetComponent<SpriteRenderer>();
        reflectionSprite = reflection.GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        reflectionAnim = reflection.GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnim.SetBool("isOnGround", isOnGround);
        playerAnim.SetBool("isMoving", isMoving);
        reflectionAnim.SetBool("isOnGround", isOnGround);
        reflectionAnim.SetBool("isMoving", isMoving);

        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        if (horizontalInput > 0 && !faceRight || horizontalInput < 0 && faceRight)
        {
            changeDirection();
        }

        if (horizontalInput != 0)
        {
            isMoving = true;
            
        } else
        {
            isMoving = false;
        }
 

        if (Input.GetButtonDown("Jump") && isOnGround && Time.time > jumpDelay)
        {
            jump = true;
            playerAudio.PlayOneShot(jumpSFX);
            jumpDelay = Time.time + 1f;
        }

        if (transform.position.x < xMin)
        {
            transform.position = new Vector2(xMin, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.position = reflection.transform.position;
            transform.Rotate(new Vector3(0, 0, 180));
            playerSprite.flipX = !playerSprite.flipX;
            reflectionSprite.flipX = !reflectionSprite.flipX;
            playerRb.gravityScale *= -1;
            playerRb.velocity = new Vector2(0, 0);
        }

        if (isOnGround && horizontalInput != 0)
        {
            if (!playerAudio.isPlaying)
            {
                playerAudio.PlayOneShot(runSFX);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    void changeDirection()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        faceRight = !faceRight;
    }

    private void FixedUpdate()
    {
        playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);
        
        if (jump)
        {
            playerRb.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
            isOnGround = false;
            playerAnim.SetBool("jump", jump);
            reflectionAnim.SetBool("jump", jump);
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mirror") || collision.gameObject.CompareTag("Obstacles"))
        {
            isOnGround = true;
            if (jump)
            {
                playerRb.velocity = new Vector2(0, 0);
            }
            playerAnim.SetBool("jump", jump);
            reflectionAnim.SetBool("jump", jump);
            
        } 


    }
}
