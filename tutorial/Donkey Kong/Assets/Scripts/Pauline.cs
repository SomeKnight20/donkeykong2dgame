using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauline : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.Win();
        }
    }
}
