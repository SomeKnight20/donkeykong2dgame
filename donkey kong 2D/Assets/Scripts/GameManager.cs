using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Win()
    {
        Debug.Log("Victory");
        ResetLevel();
    }
    public void Lose()
    {
        Debug.Log("Game over");
        ResetLevel();
    }
}
