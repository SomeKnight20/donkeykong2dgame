using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public Animator animator;

    private Rigidbody2D barrel;
    public float moveSpeed = 4;

    private Vector2 movement;

    float horizontalDir;

    private bool isFalling = false;
    public bool isGoingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        barrel = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingRight)
        {
            horizontalDir = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            horizontalDir = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        movement.x = horizontalDir * moveSpeed;

        animator.SetBool("isFalling", isFalling);
    }

    void FixedUpdate()
    {
        barrel.position += movement * Time.fixedDeltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bound"))
        {
            isGoingRight = !isGoingRight;
        }
    }
}
