using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    private Rigidbody2D player;
    public float moveSpeed;
    public float jumpForce;

    private Vector2 movement;
    public bool grounded;
    private bool canClimb = false;
    private bool isClimbing = false;

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
            if (verticalInput != 0)
            {
                isClimbing = true;
            }
            if (isClimbing)
            {
                movement.y = verticalInput * moveSpeed;
                player.velocity = Vector2.zero;
                if (!grounded)
                {
                    movement.x = 0;
                    horizontalInput = 0;
                }
                transform.localScale = new Vector3(Mathf.Round(Mathf.Abs(player.position.y)%1)*2-1, 1, 1);
            }
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
        if (!isClimbing)
        {
            animator.SetBool("grounded", grounded);
        }
        else
        {
            animator.SetBool("grounded", true);
            
        }

        animator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate()
    {
        player.position += movement * Time.fixedDeltaTime;
        //player.MovePosition(player.position + movement * Time.fixedDeltaTime);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            grounded = true;
        }
        if (isClimbing && !grounded)
        {
            if (collision.gameObject.tag == "Ladder")
            {
                player.position = new Vector2(collision.transform.position.x, player.position.y);
            }
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            grounded = false;
        }
        if (collision.gameObject.tag == "Ladder")
        {
            canClimb = false;
            isClimbing = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            canClimb = true;
        }
        if (collision.gameObject.tag == "Platform")
        {
            isClimbing = false;
        }
    }
}
