using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UserInterface : MonoBehaviourSingleton<UserInterface>
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Button playAgain;
    [SerializeField] private Text scoreText;
    [SerializeField] private Slider hp_bar;
    [SerializeField] private Player player;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private  bool isPaused;
    

    private void Awake()
   {
        base.Awake();
        player.OnHPChange    += HandlingHPChange;
        player.OnScoreChange += HandlingScoreChange;
        player.OnHPChange    += HandlingGameOver;
       

   }
        // Start is called before the first frame update
    void Start()
    {
        hp_bar.maxValue = player.GetMaxHP();
        hp_bar.value = player.GetMaxHP();
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
            gameOver.SetActive(true);
            playAgain.gameObject.SetActive(true);

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
   

}
