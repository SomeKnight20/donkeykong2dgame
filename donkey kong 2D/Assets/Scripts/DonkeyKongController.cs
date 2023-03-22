using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyKongController : MonoBehaviour
{
    public Animator animator;
    public Transform throwPoint;
    public GameObject barrelPrefab;

    public bool testThrowing = false;

    private float timer = 0f;
    public float timeBetweenThrows = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(testThrowing){
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("dk_throw"))
            {
                timer += Time.deltaTime;
            }
            if (timer > timeBetweenThrows)
            {
                timer = 0f;
                animator.SetTrigger("throw");
            }
        }
        
    }

    public void ThrowBarrel()
    {
        Instantiate(barrelPrefab, throwPoint.position, throwPoint.rotation);
    }
}
