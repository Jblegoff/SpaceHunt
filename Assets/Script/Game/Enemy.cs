using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] float speed;
    Camera m_camera;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] private float timeBtwShot;
    public float startTimeBtwShot;
    bool MovingRight=true;
    [SerializeField] float frequency = 20f;
    public float magnitude = 0.5f;
    Vector3 pos,localScale;
    Vector3 screenpos; 
    public override void Awake()
    {
        base.Awake();
        m_camera = Camera.main; 
        
    }
    // Start is called before the first frame update
    void Start()
    {
        timeBtwShot = startTimeBtwShot;
        localScale = transform.localScale;
        pos = transform.position;
        screenpos= m_camera.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
       
        
        
        CheckWhereToMove();
        if (MovingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
        
        if (timeBtwShot <= 0 )
        {
            Instantiate(enemyBullet, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
        
    }

    private void MoveRight()
    {
        pos += transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    private void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    private void CheckWhereToMove()
    {
        if (transform.position.x < -7f)
            MovingRight = true;
        else if (transform.position.x > 7f)
            MovingRight = false;
        if (((MovingRight) && (localScale.x < 0)) || ((!MovingRight) && (localScale.x > 0))) ;
               localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Movement()
    {
        
        if (screenpos.y <= 0) 
            Destroy(gameObject, 0f);
        if (screenpos.y > Screen.height / 2) 
            transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    
    
    void OnCollisionEnter(Collision collision)
    {
        // Debug-draw all contact points and normals
        if (collision.rigidbody.tag == "Player") // the ennemy crashes if he collides with player
        {
            Destroy(gameObject);
        }

        if (collision.rigidbody.tag == "Bullet") // the ennemy crashes if he collides with player
        {
            loseHP(1);
            //Debug.Log("Ennemy HP : " + Current_HP);
        }

        if (Current_HP <= 0)  // If the player has nno HP , he dies 
        {
            Destroy(gameObject);
        }
    }
    }
