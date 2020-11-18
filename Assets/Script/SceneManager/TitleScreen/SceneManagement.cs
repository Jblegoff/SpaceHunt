using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    
    // Start is called before the first frame update

   public void LaunchGame()
    {
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
        
    }

    public void LaunchScore()
    {
        SceneManager.LoadSceneAsync("Score Screen", LoadSceneMode.Single);
    }
    public void LaunchCredit()
    {
        SceneManager.LoadSceneAsync("Credit", LoadSceneMode.Single);
        
    } 
    public void ReturnToTitle()
    {
        SceneManager.LoadSceneAsync("Title Screen", LoadSceneMode.Single);
       
    }

   public void ExitGame()
    {
        Application.Quit();
    }

}
