using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBarrel : MonoBehaviour
{
    public Animator animator;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("burned");
        }
    }
}
