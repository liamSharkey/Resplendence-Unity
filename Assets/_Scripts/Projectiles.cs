using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float movementSpeed = 6;
    public float damage = 10;

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
        Statue s = collision.GetComponent<Statue>();
        if(s != null){
            StartCoroutine(s.Die());
        }
        IllusionScript I = collision.GetComponent<IllusionScript>();
        if (I != null)
        {
            StartCoroutine(I.Die());
        }
        if (collision.gameObject.name == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().health -= damage;

        }
        if (collision.gameObject.name != "Player")
        {
            Destroy(gameObject);
        }
    }
}
