using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bullet : MonoBehaviour
{ 
    [SerializeField] float speed;
    float height = Screen.height;
    Camera camera;
    public delegate void OnBulletHitEvent(int score_amount);
    public event OnBulletHitEvent OnBulletHit;
    public int Score { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }

    void Awake()
    {
        camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 screenpos = camera.WorldToScreenPoint(this.transform.position);
        if(screenpos.y>=height) Destroy(gameObject, 0f);
        else transform.Translate( Vector3.up * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        // Debug-draw all contact points and normals
        if (collision.rigidbody.tag == "Enemy")
        {
            //UnityEngine.Debug.Log(collision.rigidbody.tag);
            Score += 100;
            if (OnBulletHit != null)
            {
                OnBulletHit(Score);
            }
            Destroy(this.gameObject);
        }
    }
}
