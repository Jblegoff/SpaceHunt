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
    Transform player;
    

   public override void Awake()
    {
        base.Awake();
        m_camera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShot = startTimeBtwShot;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
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

    void Movement()
    {
        Vector3 screenpos = m_camera.WorldToScreenPoint(transform.position);
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
