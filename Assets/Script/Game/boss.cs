using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : Entity
{
    [SerializeField] float speed;
    //[SerializeField] private Player player;
    public GameObject eBullet;
    [SerializeField] private float shootFrenquency;
    [SerializeField] Camera m_camera;
 

    public void Awake()
    {
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
       
        Vector3 screenpos = m_camera.WorldToScreenPoint(transform.position);
        
        if (screenpos.x > 0 && screenpos.x < 795 && screenpos.y > 0 && screenpos.y < 465)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        else
        {
            transform.Rotate(Vector3.up * Random.Range(20, 370));
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Bullet")
        {
            loseHP(1);
        }
        if (Current_HP <= 0) Destroy(gameObject);
    }
}
