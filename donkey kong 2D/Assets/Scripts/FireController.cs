using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D fire;
    public float moveSpeed = 3;
    private Vector2 movement;
    float horizontalDir;

    private bool isClimbing = false;
    private float climbDir = 0;
    private Transform ladder;

    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponent<Rigidbody2D>();
        horizontalDir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (horizontalDir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalDir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (isClimbing){
            if(fire.position.y < ladder.transform.GetChild(1).transform.position.y+0.8f){
                StopClimbing();
            }
        }else{
            if(Random.Range(0, 500) == 0){
                horizontalDir = -horizontalDir;
            }
        }

        movement.x = horizontalDir * moveSpeed;
        movement.y = climbDir * moveSpeed;
    }

    void FixedUpdate()
    {
        fire.position += movement * Time.fixedDeltaTime;
    }

    public void DestroyFire(){
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bound"))
        {
            horizontalDir = -horizontalDir;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GameOver();
        }
        // if (collision.gameObject.CompareTag("Barrel"))
        // {
        //     collision.gameObject.GetComponent<BarrelController>().DestroyBarrel();
        // }
    }

    void StartClimbing(Transform ladderPos){
        isClimbing = true;
        fire.isKinematic = true;
        fire.position = new Vector2(ladderPos.position.x, fire.position.y);
        horizontalDir = 0;
        if(fire.position.y > ladderPos.position.y){
            climbDir = -1;
        }else{
            climbDir = 1;
        }
        ladder = ladderPos;
    }
    void StopClimbing(){
        isClimbing = false;
        fire.isKinematic = false;
        climbDir = 0;
        horizontalDir = Random.Range(0, 2)*2-1;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Ladder") && Random.Range(0, 2) == 0)
        {
            StartClimbing(collision.transform);
        }
        if (isClimbing)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().GameOver();
            }
        }
        if (collision.gameObject.CompareTag("Barrel"))
        {
            collision.gameObject.GetComponent<BarrelController>().DestroyBarrel();
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if (isClimbing)
        {
            if (collision.gameObject.CompareTag("Ladder"))
            {
                StopClimbing();
            }
        }
    }
}
