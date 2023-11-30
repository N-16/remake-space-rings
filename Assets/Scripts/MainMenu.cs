using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public void StartGame()
    {
         
        SceneManager.LoadScene(2);
    }
    public void StartGameWithAI(){
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}