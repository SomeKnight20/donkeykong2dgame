using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    public float moveSpeed;
    public float jumpForce;

    private Vector2 movement;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown("space")
            && grounded)
        {
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
        grounded = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }
}
