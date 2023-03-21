using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    public GameManager gameManager;
    public Animator animator;

    private Rigidbody2D player;
    private BoxCollider2D boxCollider2D;
    public float moveSpeed;
    public float jumpForce;

    private float playerHeight;

    private Vector2 movement;
    private bool grounded;
    private bool canClimb = false;
    private bool isClimbing = false;
    private bool isDead = false;

    float horizontalInput;
    float verticalInput;

    // private float timer = 0f;
    private bool holdingHammer = false;


    private Transform ladder;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        playerHeight = animator.GetComponent<SpriteRenderer>().size.y;
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


            
            movement.y = 0;
            if (canClimb){
                if (verticalInput != 0 && grounded){
                    isClimbing = true;
                    horizontalInput = 0;
                }
            }else{
                isClimbing = false;
            }
            if (isClimbing){
                if(player.position.y < ladder.transform.GetChild(1).transform.position.y+playerHeight/2 
                || player.position.y > ladder.transform.GetChild(0).transform.position.y-playerHeight/2){
                    if(horizontalInput != 0){
                        isClimbing = false;
                    }
                }
                if (player.position.y <= ladder.transform.GetChild(0).transform.position.y+playerHeight/2
                && player.position.y >= ladder.transform.GetChild(1).transform.position.y){
                    player.velocity = Vector2.zero;
                    player.isKinematic = true;
                    movement.y = verticalInput * moveSpeed;
                    player.position = new Vector2(ladder.transform.position.x, player.position.y);
                    transform.localScale = new Vector3(Mathf.Round(Mathf.Abs(player.position.y) % 1) * 2 - 1, 1, 1);
                }else{
                    // Debug.Log("Test");
                    isClimbing = false;
                }
            }else{
                player.isKinematic = false;
            }
            movement.x = horizontalInput * moveSpeed;
            // if (canClimb)
            // {
            //     if (verticalInput != 0 && grounded)
            //     {
            //         isClimbing = true;
            //     }
            //     if (isClimbing)
            //     {
            //         player.isKinematic = true;
            //         movement.y = verticalInput * moveSpeed;
            //         if (!grounded)
            //         {
            //             movement.x = 0;
            //             horizontalInput = 0;
            //         }
            //         transform.localScale = new Vector3(Mathf.Round(Mathf.Abs(player.position.y) % 1) * 2 - 1, 1, 1);
            //     }
            // }


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


    public void GetHammer(){
        holdingHammer = true;
    }
    public void GameOver(){
        isDead = true;
        Destroy(player);
        gameManager.Lose();
    }

    // private bool IsGrounded()
    // {
    //     return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayerMask);
    // }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = true;
            ladder = collision.transform;
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
            // isClimbing = false;
        }
    }
}
