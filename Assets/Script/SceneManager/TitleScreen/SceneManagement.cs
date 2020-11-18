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
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        
    }

    public void LaunchScore()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void LaunchCredit()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
        
    } 
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
       
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

   public void ExitGame()
    {
        Application.Quit();
    }

}
