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
        SceneManager.LoadScene("Level1");
    }

    public void LaunchScore()
    {
        SceneManager.LoadScene("Score Screen");
    }
    public void LauchCredit()
    {
        SceneManager.LoadScene("Credit");
    } 
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title Screen");
    }

   

}
