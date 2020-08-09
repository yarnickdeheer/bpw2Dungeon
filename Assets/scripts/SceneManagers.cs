using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagers : MonoBehaviour
{
    //start the game
    public void StartScene()            
    {
        SceneManager.LoadScene("START");
    }
    // stop the game
    public void stopGame()
    {
        Application.Quit();
    }
    // reload the game scene
    public void again()
    {
        SceneManager.LoadScene("game");
    }
}
