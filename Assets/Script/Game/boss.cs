﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boss : Entity
{
    [SerializeField] float speed;
    public GameObject BossBullet;
    [SerializeField] private float shootFrenquency;
    [SerializeField] Camera m_camera;
    Stopwatch stopwatch = new Stopwatch();
    public Vector3 target;

    bool m_isDestroyed = false;
    public delegate void OnHPChangeEvent(int hp);
    public event OnHPChangeEvent OnHPChange;
    public void Awake()
    {
        base.Awake();
        stopwatch.Start();
        m_camera = Camera.main;
        target = new Vector3(UnityEngine.Random.Range(-6.0f, 6.0f), UnityEngine.Random.Range(-4.0f, 4.0f), 0.0f);

    }
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //random movement of the boss thanks to a target

        UnityEngine.Debug.Log("target: " + target);
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (target == transform.position)
        {
            target.x = UnityEngine.Random.Range(-6.0f, 6.0f);
            target.y = UnityEngine.Random.Range(-4.0f, 4.0f);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        //Generation of the boss's bullets
        stopwatch.Stop();
        TimeSpan ts = stopwatch.Elapsed;
        if (ts.TotalSeconds > shootFrenquency)
        {
            stopwatch.Reset();
            stopwatch.Start();
            GameObject Bullet_instance = Instantiate(BossBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Bullet_instance.GetComponent<BossBullet>();
        }
        else stopwatch.Start();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Bullet")
        {
            OnHPChange?.Invoke(1);
            loseHP(1);
        }
        if (Current_HP <= 0) Destroy(gameObject);
    }
    
    public int GetHP()
    {
        return Current_HP;
    }
}
