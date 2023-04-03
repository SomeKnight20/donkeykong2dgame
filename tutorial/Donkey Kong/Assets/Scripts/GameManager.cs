using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
