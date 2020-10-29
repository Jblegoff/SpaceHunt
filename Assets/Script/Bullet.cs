using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    [SerializeField] float speed;
    Vector2 screenBounds;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.position.y > screenBounds.y || rigidbody.position.y < -screenBounds.y)
        {
            Destroy(gameObject);
        }
    }
    

}
