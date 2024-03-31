using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondBossProjectile : MonoBehaviour
{
    public float minSpeed = -3; 
    public float maxSpeed = -15; 
    public Sprite[] projectileSprites;

    public int minDamage = 1; 
    public int maxDamage = 8; 

    private float movementSpeed;
    private float aliveTime;
    public float lifespan = 2f;

    void Start()
    {
        movementSpeed = Random.Range(minSpeed, maxSpeed);
        Debug.Log("Projectile Speed: " + movementSpeed);
        aliveTime = Time.time;
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 365));

        Sprite selectedSprite = projectileSprites[Random.Range(0, projectileSprites.Length)];
        GetComponent<SpriteRenderer>().sprite = selectedSprite;
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
            int damage = Random.Range(minDamage, maxDamage + 1); 
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(damage);
        }
        if (collision.gameObject.name != "Boss")
        {
            Destroy(gameObject);
        }
    }
}
