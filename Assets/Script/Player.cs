using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] Camera m_MainCamera;
    [SerializeField] float m_verticalSpeed;
    [SerializeField] float m_horizontalSpeed;
    public GameObject Bullet;
    [SerializeField] float shootFrenquency;
    Stopwatch stopwatch=new Stopwatch();
    float height = Screen.height;
    float width = Screen.width;
    public int playerScore { get; set; }
    public delegate void OnHPChangeEvent(int hp);
    public event OnHPChangeEvent OnHPChange;

    public delegate void OnScoreChangeEvent(int score);
    public event OnScoreChangeEvent OnScoreChange;
    // Start is called before the first frame update
    void Start()
    {
        
        m_verticalSpeed = 8.0f;
        m_horizontalSpeed = 8.0f;

        
    }
    void Awake()
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
        Vector3 ScreenPosition = m_MainCamera.WorldToScreenPoint(transform.position);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (ScreenPosition.x < 0) return;
            this.transform.position += Vector3.left * m_horizontalSpeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (ScreenPosition.x > width) return;
            transform.position += Vector3.right * m_horizontalSpeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        {
            if (ScreenPosition.y > height) return;
            transform.position += Vector3.up * m_verticalSpeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (ScreenPosition.y < 0) return;
            transform.position += Vector3.down * m_verticalSpeed * Time.deltaTime;
        }
        if ( Input.GetKey(KeyCode.Space))
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            if (ts.TotalSeconds > shootFrenquency)
            {
                stopwatch.Reset();
                stopwatch.Start();
                Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            }
            else stopwatch.Start();
            
           
        }
        
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Enemy")
        {
            OnHPChange?.Invoke(1);
            this.loseHP(1);
            UnityEngine.Debug.Log("Player HP: " + Current_HP);
        }
        if (Current_HP <= 0) Destroy(gameObject);
    }

    private void OnBulletHitPalyer(int score)
    {
        playerScore += score;
        OnScoreChange?.Invoke(score);
        UnityEngine.Debug.Log("SCORE: " + playerScore);
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
