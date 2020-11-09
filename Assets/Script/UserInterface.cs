using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Button playAgain;
    [SerializeField] private Text scoreText;
    [SerializeField] private Slider hp_bar;
    [SerializeField] private Player player;
    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        player.OnHPChange += HandlingHPChange;
        player.OnScoreChange += HandlingScoreChange;
        player.OnHPChange += HandlingGameOver;

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
    }
    private void HandlingHPChange(int hp)
    {
        hp_bar.value = player.GetHP() - hp;
        Debug.Log("Current bar value: " + hp_bar.value + ", Player's HP: " + player.GetHP() + ", MAX HP: " + player.GetMaxHP());
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
    private void TooglePlayPause(bool pause)
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pause = true;
        if (pause) pausePanel.SetActive(true);

        else pausePanel.SetActive(false);
        pause = false;
    }


}