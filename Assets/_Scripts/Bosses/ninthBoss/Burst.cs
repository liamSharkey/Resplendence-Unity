using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour
{
    public float movementSpeed = -8;

    private float aliveTime;
    public float lifespan = 4f;
    void Start()
    {
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
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(5);
        }

    }
}
