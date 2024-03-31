using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondBossProjectile : MonoBehaviour
{
    public float minSpeed = -4;
    public float maxSpeed = -12;
    public Sprite[] projectileSprites;

    public int minDamage = 1;
    public int maxDamage = 7;

    public float splitDistance = 5f; 
    public bool canSplit = true; 

    private float movementSpeed;
    private float aliveTime;
    private float traveledDistance = 0f;
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
        float moveStep = movementSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0, moveStep, 0));
        traveledDistance += Mathf.Abs(moveStep);

        if (traveledDistance >= splitDistance && canSplit)
        {
            SplitProjectile();
        }

        if (Time.time - aliveTime > lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void SplitProjectile()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject newProjectile = Instantiate(gameObject, transform.position, Quaternion.identity);
            secondBossProjectile newProjectileScript = newProjectile.GetComponent<secondBossProjectile>();
            newProjectileScript.canSplit = false;
        }

        Destroy(gameObject); 
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
