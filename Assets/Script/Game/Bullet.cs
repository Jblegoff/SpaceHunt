using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{ 
    [SerializeField] float speed;
    float height = Screen.height;
    Camera m_camera;
    public delegate void OnBulletHitEvent(int score_amount);
    public event OnBulletHitEvent OnBulletHit;
    public int score { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
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
        if (screenpos.y >= height) Destroy(gameObject, 0f);
        else transform.Translate( Vector3.up * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.rigidbody.tag == "Enemy")
        {
            score += 200;
            //Debug.Log("score " + score);
            OnBulletHit?.Invoke(score);
            Destroy(gameObject);
        }
        if(collision.rigidbody.tag == "Boss")
        {
            score += 300;
            OnBulletHit?.Invoke(score);
            Destroy(gameObject);
        }
    }
}
