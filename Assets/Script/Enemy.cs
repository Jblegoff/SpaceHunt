using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] GameObject traget;
    [SerializeField] float speed;
    float m_height = Screen.height;
    Camera m_camera;

   public override void Awake()
    {
        base.Awake();
        m_camera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 screenpos = m_camera.WorldToScreenPoint(this.transform.position);
        if (screenpos.y < 0) Destroy(gameObject, 0f);
        if (screenpos.y > 0) transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        // Debug-draw all contact points and normals
        if (collision.rigidbody.tag == "Player") // the ennemy crashes if he collides with player
        {
            Destroy(this.gameObject);
        }

        if (collision.rigidbody.tag == "Bullet") // the ennemy crashes if he collides with player
        {
            this.loseHP(1);
            Debug.Log("Ennemy HP : " + Current_HP);
        }

        if (Current_HP <= 0)  // If the player has nno HP , he dies 
        {
            Destroy(this.gameObject);
        }
    }
    }
