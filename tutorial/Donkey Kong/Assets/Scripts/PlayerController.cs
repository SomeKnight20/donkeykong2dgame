using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameManager gameManager;
    private Rigidbody2D player;

    public GameObject hammerHitbox;

    float horizontalInput;
    float verticalInput;

    private Vector2 movement;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private bool grounded;
    private bool canClimb = false;
    private bool isClimbing = false;
    private bool isDead = false;

    private Transform ladder;
    private float playerHeight;


    public float hammerTime = 6f;
    private float timer = 0f;
    private bool holdingHammer = false;


    // Start is called before the first frame update
    void Start()
    {
        timer = hammerTime;
        hammerHitbox.SetActive(false);
        player = GetComponent<Rigidbody2D>();
        playerHeight = GetComponent<SpriteRenderer>().size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingHammer)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = hammerTime;
                hammerHitbox.SetActive(false);
                holdingHammer = false;
            }
            animator.SetFloat("hammerTimer", timer);
        }
        if (grounded){
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
        if(!holdingHammer){
            verticalInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown("space") && grounded && !isClimbing){
                player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            movement.y = 0;
            if (canClimb){
                if (verticalInput != 0 && grounded && horizontalInput == 0){
                    isClimbing = true;
                }
            }else{
                isClimbing = false;
            }
            if(horizontalInput != 0){
                isClimbing = false;
            }

            if (isClimbing){
                if (player.position.y <= ladder.transform.GetChild(0).transform.position.y+playerHeight/2
                    && player.position.y >= ladder.transform.GetChild(1).transform.position.y-0.1f){
                    player.velocity = Vector2.zero;
                    player.isKinematic = true;
                    movement.y = verticalInput * moveSpeed;
                    player.position = new Vector2(ladder.transform.position.x, player.position.y);
                    transform.localScale = new Vector3(Mathf.Round(Mathf.Abs(player.position.y) % 1) * 2 - 1, 1, 1);
                }else{
                    isClimbing = false;
                }
            }else{
                player.isKinematic = false;
            }
        }

        if (horizontalInput > 0){
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        

        movement.x = horizontalInput * moveSpeed;

        animator.SetFloat("speed", Mathf.Abs(horizontalInput));
        animator.SetBool("climbing", isClimbing);
        animator.SetBool("grounded", grounded);
    }

    void FixedUpdate()
    {
        if(!isDead){
            player.position += movement * Time.fixedDeltaTime;
        }
    }
    public void GetHammer(){
        hammerHitbox.SetActive(true);
        holdingHammer = true;
        animator.SetTrigger("holdHammer");
    }
    public void GameOver(){
        if(!isDead && !holdingHammer){
            isDead = true;
            animator.SetTrigger("gameOver");
        }
    }
    public void EndGame(){
        gameManager.Lose();
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
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (holdingHammer)
        {
            if (collision.gameObject.CompareTag("Barrel"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
