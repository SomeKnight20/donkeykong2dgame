using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    public GameManager gameManager;
    public Animator animator;

    private Rigidbody2D player;
    public GameObject hammerHitbox;
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

    public float hammerTime = 6f;
    private float timer = 0f;
    private bool holdingHammer = false;


    private Transform ladder;

    // Start is called before the first frame update
    void Awake()
    {
        timer = hammerTime;
        player = GetComponent<Rigidbody2D>();
        playerHeight = animator.GetComponent<SpriteRenderer>().size.y;
        hammerHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
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
            if (grounded)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
            }
            verticalInput = Input.GetAxisRaw("Vertical");


            if (Input.GetKeyDown("space")
                && grounded
                && !isClimbing
                && !holdingHammer)
            {
                player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }


            
            movement.y = 0;
            if (canClimb && !holdingHammer){
                if (verticalInput != 0 && grounded && horizontalInput == 0){
                    isClimbing = true;
                    horizontalInput = 0;
                }
            }else{
                isClimbing = false;
            }
            if(horizontalInput != 0){
                isClimbing = false;
            }
            if (isClimbing){
                if (player.position.y <= ladder.transform.GetChild(0).transform.position.y+playerHeight/2
                && player.position.y >= ladder.transform.GetChild(1).transform.position.y+playerHeight/2){
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
            movement.x = horizontalInput * moveSpeed;


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
            animator.SetBool("isHoldingHammer", holdingHammer);
        }
        animator.SetBool("isDead", isDead);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            player.position += movement * Time.fixedDeltaTime;
        }
    }


    public void GetHammer(){
        hammerHitbox.SetActive(true);
        holdingHammer = true;
        animator.SetTrigger("holdHammer");
    }
    public void GameOver(){
        if(!holdingHammer){
            isDead = true;
            Destroy(player);
            // gameManager.Lose();
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
                collision.gameObject.GetComponent<BarrelController>().DestroyBarrel();
            }
            if (collision.gameObject.CompareTag("Fire"))
            {
                collision.gameObject.GetComponent<FireController>().DestroyFire();
            }
        }
        
    }
}
