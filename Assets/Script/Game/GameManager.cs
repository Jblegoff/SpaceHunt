using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    
    public void ResetLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single); 
    }
    public void ReturnToTitle()
    {
       
         SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
         
    }
}
