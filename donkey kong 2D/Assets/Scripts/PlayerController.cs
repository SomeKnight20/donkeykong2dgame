using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public Animator animator;

    private Rigidbody2D player;
    public float moveSpeed;
    public float jumpForce;

    private Vector2 movement;
    public bool grounded;
    private bool canClimb = false;
    private bool isClimbing = false;
    private bool isDead = false;

    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (grounded)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
            }
            verticalInput = Input.GetAxisRaw("Vertical");


            if (Input.GetKeyDown("space")
                && grounded
                && !isClimbing)
            {
                player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }


            movement.x = horizontalInput * moveSpeed;
            movement.y = 0;
            if (canClimb)
            {
                if (verticalInput != 0 && grounded)
                {
                    isClimbing = true;
                }
                if (isClimbing)
                {
                    player.velocity = Vector2.zero;
                    // player.isKinematic = true;
                    player.gravityScale = 0;
                    movement.y = verticalInput * moveSpeed;
                    if (!grounded)
                    {
                        movement.x = 0;
                        horizontalInput = 0;
                    }
                    transform.localScale = new Vector3(Mathf.Round(Mathf.Abs(player.position.y) % 1) * 2 - 1, 1, 1);
                }
            }
            if (!isClimbing)
            {
                // player.isKinematic = false;
                player.gravityScale = 0.9f;
            }


            if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            animator.SetFloat("speed", Mathf.Abs(horizontalInput));
            animator.SetBool("isClimbing", isClimbing);
            animator.SetBool("grounded", grounded);
        }
        animator.SetBool("isDead", isDead);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            player.position += movement * Time.fixedDeltaTime;
        }
        
        //player.MovePosition(player.position + movement * Time.fixedDeltaTime);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
        if (isClimbing && !grounded)
        {
            if (collision.gameObject.CompareTag("Ladder"))
            {
                player.position = new Vector2(collision.transform.position.x, player.position.y);
            }
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = false;
            isClimbing = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
        {
            isDead = true;
            Destroy(player);
            gameManager.Lose();
        }
    }
}
