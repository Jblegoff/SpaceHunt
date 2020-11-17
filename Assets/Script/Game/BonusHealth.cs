using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHealth : MonoBehaviour
{
    [SerializeField] GameObject _player;
    Player player;
   
    Camera m_camera;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        player = _player.GetComponent<Player>();
        m_camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Fall();
    }

    void Fall()
    {
        Vector3 screenpoint = m_camera.WorldToScreenPoint(transform.position);
        if (screenpoint.y <= 0) Destroy(gameObject);
        else transform.Translate(Vector3.down * 0.2f * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Player")
        {
            Debug.Log("player hit");
            Destroy(gameObject);
            
        }
    }
}
