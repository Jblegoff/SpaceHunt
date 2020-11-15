using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] float speed;
    Camera m_camera;
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.Find("Player");
    }

    void Awake()
    {
        m_camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Movement());
        Movement();
    }

    void Movement()
    {

        Vector3 screenpos = m_camera.WorldToScreenPoint(transform.position);
        if (screenpos.y >= Screen.height || screenpos.y <= 0 || screenpos.x >= Screen.width || screenpos.x <= 0) Destroy(gameObject, 0f);
        else transform.Translate( player.transform.position* speed * Time.deltaTime);
        //yield return new WaitForSeconds(1.0f);
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.rigidbody.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}