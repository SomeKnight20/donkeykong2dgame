using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyKongController : MonoBehaviour
{
    public Animator animator;
    public Transform throwPoint;
    public GameObject barrelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("dk_throw"))
        {
            //if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            //{
            //    Instantiate(barrelPrefab, throwPoint.position, throwPoint.rotation);
            //}
        }
        else
        {
            animator.SetTrigger("throw");
        }
    }
}
