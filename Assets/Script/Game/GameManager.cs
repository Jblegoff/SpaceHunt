using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    
    public void ResetLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
      
        SceneManager.LoadSceneAsync(scene.name);
        
       
    }
    public void ReturnToTitle()
    {
       
         SceneManager.LoadSceneAsync("Title Screen",LoadSceneMode.Single);
         
    }
}
