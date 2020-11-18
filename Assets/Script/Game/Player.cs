using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] Camera m_MainCamera;
    [SerializeField] float m_Speed;
    public GameObject Bullet;
    private String Playername;
    [SerializeField] private float shootFrenquency;
    Stopwatch stopwatch = new Stopwatch();

    public int playerScore { get; set; }
    public delegate void OnHPLossEvent(int hp);
    public event OnHPLossEvent OnHPLoss;
    public delegate void OnHPRestoredEvent(int hp);
    public event OnHPRestoredEvent OnHPRestored;

    public delegate void OnScoreChangeEvent(int score);
    public event OnScoreChangeEvent OnScoreChange;
    // Start is called before the first frame update
    void Start()
    {

    }
    public override void Awake()
    {
        base.Awake();
        stopwatch.Start();
        m_MainCamera = Camera.main;
        playerScore = 0;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerControl();
    }


    void PlayerControl()

    {

        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (keyboard == null || mouse == null) return;

        var gamepad = Gamepad.current;
        if (gamepad == null) return; //no gamepad connected


        Vector3 moveDirection = gamepad.leftStick.ReadValue();
        Vector3 moveThisFrame = Time.deltaTime * m_Speed * ((Vector3.right * moveDirection.x) + (Vector3.up * moveDirection.y));// Make movement speed frame-rate independent
        transform.position += moveThisFrame;

        float vertical = keyboard.wKey.ReadValue()
                      - keyboard.sKey.ReadValue();
        float horizontal = keyboard.dKey.ReadValue()
                         - keyboard.aKey.ReadValue();
        // Make movement speed frame-rate independent 
        Vector3 moveFrame = Time.deltaTime * m_Speed * ((Vector3.right * horizontal) + (Vector3.up * vertical));

        transform.position += moveFrame;

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);

        if (gamepad.aButton.isPressed || keyboard.spaceKey.isPressed)
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            if (ts.TotalSeconds > shootFrenquency)
            {
                stopwatch.Reset();
                stopwatch.Start();
                GameObject Bullet_instance = Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y -1, transform.position.z), Quaternion.identity);
                Bullet_instance.GetComponent<Bullet>().OnBulletHit += OnBulletHitPlayer;
            }
            else stopwatch.Start();

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Enemy" || collision.rigidbody.tag == "Boss" || collision.rigidbody.tag == "BulletBoss"|| collision.rigidbody.tag == "EnemyBullet")
        {
            OnHPLoss?.Invoke(1);
            loseHP(1);
           // UnityEngine.Debug.Log("Player HP: " + Current_HP);
        }
        if (collision.rigidbody.tag == "Healthpack") {
            OnHPRestored?.Invoke(5);
            restoreHp(5);
        }
        if (Current_HP <= 0) Destroy(gameObject);
    }
    private void OnBulletHitPlayer(int score)
    {
        playerScore += score;
        OnScoreChange?.Invoke(score);

       // UnityEngine.Debug.Log("SCORE: " + playerScore);
    }
    public int GetHP()
    {
        return Current_HP;
    }
    public int GetScore()
    {
        return playerScore;
    }
}