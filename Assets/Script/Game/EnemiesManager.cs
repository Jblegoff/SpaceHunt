﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{

    [SerializeField] private Camera m_camera;
    [SerializeField] private GameObject UI;
    [SerializeField] Player player;
    private enum SpawnState { SPAWNING, WAITING, COUNTING }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public int count;
        public float rate;
    }
    
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Boss Boss;
    public int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;
    private float searchCountdownBoss = 5f;

    private SpawnState state = SpawnState.COUNTING;
    void Start()
    {
        waveCountdown = timeBetweenWaves;

    }
    void Awake()
    {
        m_camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            //Check if enemy alive
            if (!EnemyisAlive())
            {
                //Begin a new round
                WaveCompleted();
            }
            
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
        
    }

    private IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("SPAWNING WAVE: " + _wave.name);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    bool EnemyisAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    void WaveCompleted()
    {
        Debug.Log("WaveCompleted");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            state = SpawnState.SPAWNING;
            Debug.Log("All wave complete");
            StartCoroutine(Waiter());
            Boss.gameObject.SetActive(true);

        }
        else
        {
            nextWave++;
        }

    }

    void SpawnEnemy(GameObject _enemy)
    {
        Debug.Log("Spawning enemy: " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(2, spawnPoints.Length)];
        Instantiate(_enemy, new Vector3(_sp.position.x, _sp.position.y, 0), _sp.rotation);
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5f);
    }
}