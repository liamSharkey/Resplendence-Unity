using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float movementSpeed = 6;

    private float rotation;
    private float xRotation;
    private float yRotation;

    private float aliveTime;
    public float lifespan = 2f;

    void Start()
    {
        xRotation = transform.rotation.x * Mathf.Rad2Deg;
        yRotation = transform.rotation.y * Mathf.Rad2Deg;

        if (xRotation > 0)
        {
            rotation = 0;
        }

        else if (xRotation < 0)
        {
            rotation = 180;
        }

        else if (yRotation > 0)
        {
            rotation = 90;
        }

        else if (yRotation < 0)
        {
            rotation = -90;
        }
        transform.eulerAngles = new Vector3(0, 0, rotation);

        aliveTime = Time.time;

    }

    void Update()
    {        
        transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));

        if (Time.time - aliveTime > lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Boss")
        {
            collision.gameObject.GetComponent<firstBoss>().health -= 10;
        }
        if (collision.gameObject.name != "Player")
        {
            Destroy(gameObject);
        }
    }
}
