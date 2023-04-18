using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public Animator animator;

    private Rigidbody2D barrel;
    public float moveSpeed = 5f;

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
        
        if(isFalling){
            barrel.isKinematic = true;
            movement.x = 0;
            movement.y = -moveSpeed;
        }else{
            movement.x = horizontalDir * moveSpeed;
        }

        animator.SetBool("falling", isFalling);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("LadderTop") && Random.Range(0, 3) == 0)
        {
            isFalling = true;
            barrel.position = new Vector2(collision.transform.position.x, barrel.position.y);
        }
        if (isFalling)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().GameOver();
            }
            if (collision.gameObject.CompareTag("LadderBottom"))
            {
                isGoingRight = !isGoingRight;
                isFalling = false;
                barrel.isKinematic = false;
            }
        }
    }
}
