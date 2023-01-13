using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    private float speed = 10f;
    private float jumpSpeed = 8f;
    private float horizontalInput;
    private bool isOnGround = true;
    private Rigidbody2D playerRb;
    private float xMin = -11f;
    private bool faceRight = true;
    private bool jump = false;
    private SpriteRenderer playerSprite;

    // Reflect 
    private GameObject reflection;
    private SpriteRenderer reflectionSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        reflection = GameObject.FindGameObjectWithTag("Reflection");
        playerSprite = GetComponent<SpriteRenderer>();
        reflectionSprite = reflection.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        if (horizontalInput > 0 && !faceRight || horizontalInput < 0 && faceRight)
        {
            changeDirection();
        }
 

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            jump = true;
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
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mirror"))
        {
            isOnGround = true;
        }
    }
}
