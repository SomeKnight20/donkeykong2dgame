using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyKongController : MonoBehaviour
{
    public Animator animator;
    public Transform throwPoint;
    public GameObject barrelPrefab;

    public bool testThrowing = false;

    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(testThrowing){
            timer += Time.deltaTime;

            if (timer > 3.0f)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("dk_throw"))
                {
                    timer = 0f;
                    animator.SetTrigger("throw");
                }
            }
        }
        
    }

    public void ThrowBarrel()
    {
        Instantiate(barrelPrefab, throwPoint.position, throwPoint.rotation);
    }
}
