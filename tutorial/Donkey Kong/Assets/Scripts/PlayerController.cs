using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;

    float horizontalInput;
    float verticalInput;

    private Vector2 movement;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private bool grounded;
    private bool canClimb = false;
    private bool isClimbing = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown("space") && grounded){
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        movement.x = horizontalInput * moveSpeed;
    }

    void FixedUpdate()
    {
        player.position += movement * Time.fixedDeltaTime;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = true;
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
        }
    }
}
