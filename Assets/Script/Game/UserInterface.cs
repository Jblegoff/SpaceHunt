using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class UserInterface : MonoBehaviourSingleton<UserInterface>
{
    public GameObject gameOver;
    public GameObject victory;
    [SerializeField] private Text scoreText;
    [SerializeField] private Slider hp_bar;  
    [SerializeField] private Slider hp_boss;  
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private  bool isPaused;
    public bool isGameOver;

    

    private void Awake()
    {
        base.Awake();
        player.OnHPChange += HandlingHPChange;
        player.OnScoreChange += HandlingScoreChange;
        player.OnHPChange += HandlingGameOver;
        if (boss.enabled)
        {
            boss.OnHPChange += HandlingVictory;
            boss.OnHPChange += HandlingBossHPChange;
        }
     
    }

   

    // Start is called before the first frame update
    void Start()
    {
        hp_bar.maxValue = player.GetMaxHP();
        hp_bar.value = player.GetMaxHP();
        isGameOver = false;
        hp_boss.maxValue = boss.GetMaxHP();
        hp_boss.value = boss.GetHP();
        
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (keyboard == null || mouse == null) return;//no keyboard or mouse connected

        var gamepad = Gamepad.current;
        if (gamepad == null) return; //no gamepad connected

        if (gamepad.startButton.wasPressedThisFrame||keyboard.escapeKey.wasPressedThisFrame){
            isPaused = !isPaused;
            TooglePlayPause(isPaused);
        }
       
        
    }
    private void HandlingVictory(int hp)
    {
        if (boss.GetHP() - hp == 0)
        {
            PlayerPrefs.SetInt("score", player.GetScore());
            victory.SetActive(true);
            isGameOver = true;

        }

    } 
    private void HandlingBossHPChange(int hp)
    {
        if (boss.isActiveAndEnabled)
        {
            hp_boss.gameObject.SetActive(true);
            hp_boss.value = boss.GetHP() - hp;
        }
    }
    private void HandlingHPChange(int hp)
    {
        hp_bar.value = player.GetHP()-hp;
        //Debug.Log("Current bar value: " + hp_bar.value + ", Player's HP: " + player.GetHP()+ ", MAX HP: "+player.GetMaxHP()) ;
    }
    private void HandlingScoreChange(int score)
    {
        scoreText.text = $"Score: {player.GetScore()}";

    }
    private void HandlingGameOver(int hp)
    {
        if (player.GetHP() - hp == 0)
        {
            PlayerPrefs.SetInt("score", player.GetScore());
            gameOver.SetActive(true);
            isGameOver = true;
            
        }
        
    }
   
    public void TooglePlayPause(bool pause)
    {

        if (pause) 
        {
            Time.timeScale = 0.0f;
            pausePanel.SetActive(true);
         
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }
        
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }


}