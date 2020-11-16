using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBullet : MonoBehaviour
{
    [SerializeField] float speed;
    Camera m_camera;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f)).normalized;
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
        if (screenpos.y >= Screen.height || screenpos.y <= 0 || screenpos.x >= Screen.width || screenpos.x <= 0) Destroy(gameObject, 0f);
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.rigidbody.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}