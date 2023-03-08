using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBarrelController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("justBurned");
        }
    }
}
