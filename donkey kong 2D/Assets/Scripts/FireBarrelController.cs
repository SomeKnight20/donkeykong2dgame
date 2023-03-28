using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBarrelController : MonoBehaviour
{
    public Animator animator;
    public GameObject firePrefab;
    public Transform fireSpawnPoint;

    public bool spawnFire = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFire(){
        Instantiate(firePrefab, fireSpawnPoint.position, fireSpawnPoint.rotation);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("justBurned");
            if(spawnFire){
                SpawnFire();
            }
            
        }
    }
}
