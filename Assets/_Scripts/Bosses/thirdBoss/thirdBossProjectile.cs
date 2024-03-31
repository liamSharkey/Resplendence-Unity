using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdBossProjectile : MonoBehaviour
{
    public float movementSpeed = -6;

    private float aliveTime;
    public float lifespan = 2f;

    void Start()
    {
        aliveTime = Time.time;
        transform.eulerAngles += new Vector3(0, 0, Random.Range(-15f, 15f));
    }

    void Update()
    {
        transform.Translate(new Vector3(0, movementSpeed * Time.deltaTime, 0));

        if (Time.time - aliveTime > lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(7);
        }
        if (collision.gameObject.name != "Boss")
        {
            Destroy(gameObject);
        }

    }
}
