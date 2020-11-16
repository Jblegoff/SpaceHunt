using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet : MonoBehaviour
{
   
    [SerializeField] float speed;
    float height = Screen.height;
    Camera m_camera;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        m_camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 screenpos = m_camera.WorldToScreenPoint(transform.position);
        if (screenpos.y <= 0) Destroy(gameObject, 0f);
        else transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.rigidbody.tag == "Player")
        {           
            Destroy(gameObject);
        }
    }
}
