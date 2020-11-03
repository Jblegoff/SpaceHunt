using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemies;
    [SerializeField] float enemiesSpawnTime;
    private IEnumerator coroutine;
    [SerializeField] private Camera m_camera;
    // Start is called before the first frame update
    void Start()
    {
        if ( Application.isPlaying ) {
            coroutine = SpawnEnemies(enemiesSpawnTime);
            StartCoroutine(coroutine);
        }
    }
    void Awaker()
    {
        m_camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
             
    }
    private IEnumerator SpawnEnemies(float spawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Vector3 spawnLocation = m_camera.ScreenToWorldPoint(new Vector3(Random.Range(0, m_camera.pixelWidth), m_camera.pixelHeight, -m_camera.transform.position.z));
            Instantiate(enemies, spawnLocation, Quaternion.identity);
        }
    }
}
