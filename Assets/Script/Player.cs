using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera m_MainCamera;
    [SerializeField] float m_verticalSpeed;
    [SerializeField] float m_horizontalSpeed;
    public GameObject Bullet;
    [SerializeField] float shootFrenquency;
    Stopwatch stopwatch=new Stopwatch();   
    

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
        m_verticalSpeed = 6.0f;
        m_horizontalSpeed = 6.0f;

        
    }
    void Awake()
    {
        stopwatch.Start();
        
    }
    // Update is called once per frame
    void Update()
    {

        PlayerControl();
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = m_MainCamera.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = m_MainCamera.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = m_MainCamera.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = m_MainCamera.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3( Mathf.Clamp(transform.position.x, leftBorder, rightBorder), 
                                          Mathf.Clamp(transform.position.y, topBorder, bottomBorder), 
                                          transform.position.z );
       
    }
    void PlayerControl()

    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * m_horizontalSpeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * m_horizontalSpeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * m_verticalSpeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * m_verticalSpeed * Time.deltaTime;
        }
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            
            print(ts);
            if (ts.TotalSeconds > shootFrenquency)
            {
                stopwatch.Reset();
                stopwatch.Start();
                Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            }
            else stopwatch.Start();
            
           
        }
        
        
    }


}
