using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform topHandler;
    public Transform bottomHandler;
    // public Transform ladderTile;

    void Awake() {
        float height = GetComponent<SpriteRenderer>().size.y;
        topHandler.position    = new Vector3(transform.position.x, transform.position.y + (height / 2), 0);
        bottomHandler.position = new Vector3(transform.position.x, transform.position.y - (height / 2), 0);
        // ladderTile.position = new Vector3(transform.position.x, transform.position.y + (height / 2), 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
